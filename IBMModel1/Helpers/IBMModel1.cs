using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMModel1.Helpers
{
    class IBMModel1
    {
        protected WordByWordTranslationMapper mTranslationMap = new WordByWordTranslationMapper();

        public WordByWordTranslationMapper TranslationMap
        {
            get { return mTranslationMap; }
        }

        public IBMModel1()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a">alignment array of length modifier, where modifier is the length of input language</param>
        /// <param name="output_lang">tagged_sentence in output language in the form token array of lengthl, where l is the length in output language</param>
        /// <returns>probability of the alignment array given output language and length</returns>
        public virtual double GetProbabilityOfAlignmentGivenOutputLang(string[] input_lang, int[] a, string[] output_lang)
        {
            int l = output_lang.Length;
            int m = a.Length; //length of the input tagged_sentence
            double prob_a_given_output = 1.0 / System.Math.Pow(l + 1, m);
            return prob_a_given_output;
        }

        public virtual double GetProbabilityOfInputLangGivenAlignmentAndOutputLang(string[] input_lang, int[] a, string[] output_lang)
        {
            int m = input_lang.Length; // length of the input tagged_sentence;
            Debug.Assert(m == a.Length);
            double prob_input_given_a_and_output = 1;
            for (int i_input = 0; i_input < m; ++i_input)
            {
                int i_output = a[i_input];
                string word_input = input_lang[i_input];
                string word_output = output_lang[i_output];

                double t_input_given_output = GetProbabilityInputWordGivenOutputWord(word_input, word_output);
                prob_input_given_a_and_output *= t_input_given_output;
            }
            return prob_input_given_a_and_output;
        }

        public virtual double GetProbabilityInputWordGivenOutputWord(string word_input, string word_output)
        {
            return mTranslationMap.GetProbabilityInputWordGivenOutputWord(word_input, word_output);
        }

        public virtual int[] GetAlignment(string[] input_lang, string[] output_lang)
        {
            int m = input_lang.Length;
            int l = output_lang.Length;

            int[] a = new int[m];

            for (int j_input = 0; j_input < m; ++j_input)
            {
                string word_input = input_lang[j_input];
                double max_t_input_given_output = GetProbabilityInputWordGivenOutputWord(word_input, "NULL");
                int a_j = -1;
                for (int i_output = 0; i_output < l; ++i_output)
                {
                    string word_output = output_lang[i_output];

                    double t_input_given_output = GetProbabilityInputWordGivenOutputWord(word_input, word_output);
                    if (t_input_given_output > max_t_input_given_output)
                    {
                        max_t_input_given_output = t_input_given_output;
                        a_j = i_output;
                    }
                }
                a[j_input] = a_j;
            }

            return a;
        }

        public double GetProbabilityOfInputLangAndAlignmentGivenOutputLang(string[] input_lang, int[] a, string[] output_lang)
        {
            double prob_a_given_output = GetProbabilityOfAlignmentGivenOutputLang(input_lang, a, output_lang);
            double prob_input_given_a_and_output = GetProbabilityOfInputLangGivenAlignmentAndOutputLang(input_lang, a, output_lang);

            double prob_a_and_input_given_output = prob_a_given_output * prob_input_given_a_and_output;
            return prob_a_and_input_given_output;
        }
    }
}
