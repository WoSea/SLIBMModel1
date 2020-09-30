using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMModel1.Helpers
{
    class EMTrainingMethod
    {
        private static Random mRandom = new Random();

        public static void Initialize(IBMModel1 model, IEnumerable<EMTrainingRecord> training_corpus)
        {
            foreach (EMTrainingRecord rec in training_corpus)
            {
                string[] input_lang = rec.InputLang;
                string[] output_lang = rec.OutputLang;

                int m_input_len = input_lang.Length;
                int l_output_len = output_lang.Length;

                for (int j = 0; j < m_input_len; ++j)
                {
                    for (int a_j = 0; a_j < l_output_len; ++a_j)
                    {
                        string word_input = input_lang[j];
                        string word_output = output_lang[a_j];

                        double count_input_output = model.TranslationMap.GetCountOfInputWordAndOutputWord(word_input, word_output);
                        double count_output = model.TranslationMap.GetCountOfOutputWord(word_output);

                        model.TranslationMap.SetProbabilityInputWordGivenOutputWord(word_input, word_output, mRandom.NextDouble());
                    }
                }
            }
        }

        public static void Train(IBMModel1 model, IEnumerable<EMTrainingRecord> training_corpus, int maxIterations)
        {
            for (int iteration = 0; iteration < maxIterations; ++iteration)
            {

                foreach (EMTrainingRecord rec in training_corpus)
                {
                    string[] input_lang = rec.InputLang;
                    string[] output_lang = rec.OutputLang;

                    int m_input_len = input_lang.Length;
                    int l_output_len = output_lang.Length;

                    double[][] delta = new double[m_input_len][];
                    double sum = 0;
                    for (int j = 0; j < m_input_len; ++j)
                    {
                        delta[j] = new double[l_output_len];
                        string word_input = input_lang[j];
                        for (int a_j = 0; a_j < l_output_len; ++a_j)
                        {
                            string word_output = output_lang[a_j];
                            double t = model.TranslationMap.GetProbabilityInputWordGivenOutputWord(word_input, word_output);

                            double deltaVal = t;
                            delta[j][a_j] = deltaVal;
                            sum += deltaVal;
                        }
                    }

                    for (int j = 0; j < m_input_len; ++j)
                    {
                        for (int a_j = 0; a_j < l_output_len; ++a_j)
                        {
                            delta[j][a_j] /= sum;
                        }
                    }

                    for (int j = 0; j < m_input_len; ++j)
                    {
                        for (int a_j = 0; a_j < l_output_len; ++a_j)
                        {
                            double deltaVal = delta[j][a_j];

                            string word_input = input_lang[j];
                            string word_output = output_lang[a_j];

                            double count_input_output = model.TranslationMap.GetCountOfInputWordAndOutputWord(word_input, word_output);
                            double count_output = model.TranslationMap.GetCountOfOutputWord(word_output);

                            model.TranslationMap.SetCountOfInputWordAndOutputWord(word_input, word_output, count_input_output + deltaVal);
                            model.TranslationMap.SetCountOfOutputWord(word_output, count_output + deltaVal);

                            model.TranslationMap.UpdateProbabilityInputWordGivenOutputWord(word_input, word_output);


                        }
                    }
                }
            }
        }
    }
}
