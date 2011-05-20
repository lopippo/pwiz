﻿/*
 * Original author: Brendan MacLean <brendanx .at. u.washington.edu>,
 *                  MacCoss Lab, Department of Genome Sciences, UW
 *
 * Copyright 2011 University of Washington - Seattle, WA
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using pwiz.Common.Chemistry;
using pwiz.Skyline.Util;

namespace pwiz.Skyline.Model.Results
{
    public sealed class IsotopePeakInfo : Immutable
    {
        private readonly double _monoisotopicMassH;
        private readonly int _charge;
        private ReadOnlyCollection<MzProportion> _expectedDistribution;

        public IsotopePeakInfo(MassDistribution massDistribution,
                               double monoisotopicMassH,
                               int charge,
                               Func<double, double> calcFilterWindow,
                               double massResolution,
                               double minimumAbundance)
        {
            _monoisotopicMassH = monoisotopicMassH;
            _charge = charge;

            // Get peak center of mass values for the given resolution
            var q1FilterValues = MassDistribution.NewInstance(massDistribution, massResolution, 0).Keys.ToList();
            // Find the monoisotopic m/z and make sure it is exactly the expected number
            double monoMz = SequenceMassCalc.GetMZ(_monoisotopicMassH, _charge);
            double monoMzDist = monoMz;
            int monoMassIndex = 0;
            for (int i = 0; i < q1FilterValues.Count; i++)
            {
                double peakCenterMz = q1FilterValues[i];
                double filterWindow = calcFilterWindow(peakCenterMz);
                double startMz = peakCenterMz - filterWindow/2;
                double endMz = startMz + filterWindow;
                if (startMz < monoMz && monoMz < endMz)
                {
                    monoMzDist = q1FilterValues[i];
                    q1FilterValues[i] = monoMz;
                    monoMassIndex = i;
                    break;
                }                
            }
            // Insert a M-1 peak, even if it is not expected in the isotope mass distribution
            if (monoMassIndex == 0 && q1FilterValues.Count > 1)
            {
                // Use the delta from the original distribution monoMz to the next peak
                q1FilterValues.Insert(0, monoMz + monoMzDist - q1FilterValues[1]);
                monoMassIndex++;
            }

            // Use the filtering algorithm that will be used on real data to determine the
            // expected proportions of the mass distribution that will end up filtered into
            // peaks
            var filter = new SpectrumFilterPair(q1FilterValues[monoMassIndex]);
            filter.AddQ1FilterValues(q1FilterValues, calcFilterWindow);

            var expectedSpectrum = filter.FilterQ1Spectrum(massDistribution.Keys.ToArray(),
                                                           massDistribution.Values.ToArray());

            int startIndex = expectedSpectrum.Intensities.IndexOf(inten => inten >= minimumAbundance);
            if (startIndex == -1)
                throw new InvalidOperationException(string.Format("Minimum abundance {0} too high", minimumAbundance));
            // Always include the M-1 peak, even if it is expected to have zero intensity
            if (startIndex > monoMassIndex - 1)
                startIndex = monoMassIndex - 1;
            int endIndex = expectedSpectrum.Intensities.LastIndexOf(inten => inten >= minimumAbundance) + 1;
            int countPeaks = endIndex - startIndex;
            var expectedProportions = new float[countPeaks];
            for (int i = 0; i < countPeaks; i++)
            {
                expectedProportions[i] = (float) expectedSpectrum.Intensities[i + startIndex];
            }

            // TODO: Can this be discarded?
            // MassDistribution = massDistribution;

            MonoMassIndex = monoMassIndex - startIndex;

            // Find the base peak and fill in the masses and proportions
            var expectedPeaks = new List<MzProportion>();
            for (int i = 0; i < countPeaks; i++)
            {
                float expectedProportion = expectedProportions[i];
                expectedPeaks.Add(new MzProportion(q1FilterValues[i + startIndex], expectedProportion));
                if (expectedProportion > expectedProportions[BaseMassIndex])
                    BaseMassIndex = i;
            }
            ExpectedPeaks = expectedPeaks;
        }

        public int CountPeaks { get { return _expectedDistribution.Count; } }

        private int MonoMassIndex { get; set; }

        private int BaseMassIndex { get; set; }

        private IList<MzProportion> ExpectedPeaks
        {
            get { return _expectedDistribution; }
            set { _expectedDistribution = MakeReadOnly(value); }
        }

        public int MassIndexToPeakIndex(int massIndex)
        {
            return massIndex + MonoMassIndex;
        }

        public int PeakIndexToMassIndex(int index)
        {
            return index - MonoMassIndex;
        }

        public IEnumerable<float> ExpectedProportions
        {
            get { return _expectedDistribution.Select(mzP => mzP.Proportion); }
        }

        public float BaseMassPercent
        {
            get { return ExpectedPeaks[BaseMassIndex].Proportion; }
        }

        public float GetProportionI(int massIndex)
        {
            return ExpectedPeaks[MassIndexToPeakIndex(massIndex)].Proportion;
        }

        public double GetMZI(int massIndex)
        {
            return ExpectedPeaks[MassIndexToPeakIndex(massIndex)].Mz;
        }

        public double GetMassI(int massIndex)
        {
            // Return the original monoisotopic mass + H, if requested to maintain an exact match.
            if (massIndex == 0)
                return _monoisotopicMassH;
            // Otherwize use the charge to convert from the peak center m/z values
            return SequenceMassCalc.GetMH(ExpectedPeaks[MassIndexToPeakIndex(massIndex)].Mz, _charge);
        }

        #region object overrides

        public bool Equals(IsotopePeakInfo other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other._monoisotopicMassH.Equals(_monoisotopicMassH) &&
                other._charge == _charge &&
                ArrayUtil.EqualsDeep(other._expectedDistribution, _expectedDistribution) &&
                other.MonoMassIndex == MonoMassIndex &&
                other.BaseMassIndex == BaseMassIndex;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (IsotopePeakInfo)) return false;
            return Equals((IsotopePeakInfo) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = _monoisotopicMassH.GetHashCode();
                result = (result*397) ^ _charge;
                result = (result*397) ^ (_expectedDistribution != null ? _expectedDistribution.GetHashCodeDeep() : 0);
                result = (result*397) ^ MonoMassIndex;
                result = (result*397) ^ BaseMassIndex;
                return result;
            }
        }

        #endregion

        private struct MzProportion
        {
            public MzProportion(double mz, float proportion) : this()
            {
                Mz = mz;
                Proportion = proportion;
            }

            public double Mz { get; private set; }
            public float Proportion { get; private set; }
        }
    }
}
