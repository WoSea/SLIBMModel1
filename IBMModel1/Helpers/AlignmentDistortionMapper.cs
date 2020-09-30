using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMModel1.Helpers
{
    class AlignmentDistortionMapper
    {
        protected int CreateHash(int j, int a_j, int l_output_len, int m_input_len)
        {
            int Q = 3001; //a large prime number
            int hash = j;
            hash = Q * hash + a_j;
            hash = Q * hash + l_output_len;
            hash = Q * hash + m_input_len;
            return hash;
        }

        protected int CreateHash(int a_j, int l_output_len, int m_input_len)
        {
            int Q = 3001; //a large prime number
            int hash = a_j;
            hash = Q * hash + l_output_len;
            hash = Q * hash + m_input_len;
            return hash;
        }

        protected Dictionary<int, double> mCountOfAlignmentIndexGivenAlignmentValueAndInputLenAndOutput = new Dictionary<int, double>();
        protected Dictionary<int, double> mCountOfAlignmentValueAndInputLenAndOutputLen = new Dictionary<int, double>();
        protected Dictionary<int, double> mDistortionParameter = new Dictionary<int, double>();

        public double GetCountOfAlignmentIndexGivenAlignmentValueAndInputLenAndOutputLen(int j, int a_j, int l_output_len, int m_input_len)
        {
            int hash = CreateHash(j, a_j, l_output_len, m_input_len);
            if (mCountOfAlignmentIndexGivenAlignmentValueAndInputLenAndOutput.ContainsKey(hash))
            {
                return mCountOfAlignmentIndexGivenAlignmentValueAndInputLenAndOutput[hash];
            }
            return 0;
        }

        public double GetCountOfAlignmentValueAndInputLenAndOutputLen(int a_j, int l_output_len, int m_input_len)
        {
            int hash = CreateHash(a_j, l_output_len, m_input_len);
            if (mCountOfAlignmentValueAndInputLenAndOutputLen.ContainsKey(hash))
            {
                return mCountOfAlignmentValueAndInputLenAndOutputLen[hash];
            }
            return 0;
        }

        public void SetCountOfAlignmentValueAndInputLenAndOutputLen(int a_j, int l_output_len, int m_input_len, double count)
        {
            int hash = CreateHash(a_j, l_output_len, m_input_len);
            mCountOfAlignmentValueAndInputLenAndOutputLen[hash] = count;
        }

        public void SetCountOfAlignmentIndexGivenAlignmentValueAndInputLenAndOutputLen(int j, int a_j, int l_output_len, int m_input_len, double count)
        {
            int hash = CreateHash(j, a_j, l_output_len, m_input_len);
            mCountOfAlignmentIndexGivenAlignmentValueAndInputLenAndOutput[hash] = count;
        }

        public void UpdateDistortionParameter(int i_output, int j_input, int m_input_len, int l_output_len)
        {
            double count_j_given_aj_l_m = GetCountOfAlignmentIndexGivenAlignmentValueAndInputLenAndOutputLen(j_input, i_output, l_output_len, m_input_len);
            double count_aj_l_m = GetCountOfAlignmentValueAndInputLenAndOutputLen(i_output, l_output_len, m_input_len);

            int hash = CreateHash(i_output, l_output_len, m_input_len);

            if (count_aj_l_m != 0)
            {
                mDistortionParameter[hash] = (double)count_j_given_aj_l_m / count_aj_l_m;
            }
            else
            {
                mDistortionParameter[hash] = 0;
            }
        }

        public double GetDistortionParameter(int i_output, int j_input, int m_input_len, int l_output_len)
        {
            int hash = CreateHash(i_output, l_output_len, m_input_len);

            if (mDistortionParameter.ContainsKey(hash))
            {
                return mDistortionParameter[hash];
            }
            return 0;
        }

        public void SetDistortionParameter(int i_output, int j_input, int m_input_len, int l_output_len, double prob)
        {
            int hash = CreateHash(i_output, l_output_len, m_input_len);

            mDistortionParameter[hash] = prob;
        }
    }
}
