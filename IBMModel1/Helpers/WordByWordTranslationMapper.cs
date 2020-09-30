using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMModel1.Helpers
{
    class WordByWordTranslationMapper
    {
        protected Dictionary<string, Dictionary<string, double>> mCountOfInputWordAndOutputWord = new Dictionary<string, Dictionary<string, double>>();
        protected Dictionary<string, double> mCountOfOutputWord = new Dictionary<string, double>();

        protected Dictionary<int, double> mProbabilityInputWordGivenOutputWord = new Dictionary<int, double>();

        public double GetCountOfOutputWord(string word_output)
        {
            if (mCountOfOutputWord.ContainsKey(word_output))
            {
                return mCountOfOutputWord[word_output];
            }
            return 0;
        }

        public void SetCountOfOutputWord(string word_output, double count)
        {
            mCountOfOutputWord[word_output] = count;
        }

        public Dictionary<string, double> GetCountOfInputWordGivenOutputWord(string word_output)
        {
            Dictionary<string, double> countOfInputWordGivenOutputWord = null;
            if (mCountOfInputWordAndOutputWord.ContainsKey(word_output))
            {
                countOfInputWordGivenOutputWord = mCountOfInputWordAndOutputWord[word_output];
            }
            else
            {
                countOfInputWordGivenOutputWord = new Dictionary<string, double>();
                mCountOfInputWordAndOutputWord[word_output] = countOfInputWordGivenOutputWord;
            }
            return countOfInputWordGivenOutputWord;
        }


        public double GetCountOfInputWordAndOutputWord(string word_input, string word_output)
        {
            Dictionary<string, double> countOfInputWordGivenOutputWord = GetCountOfInputWordGivenOutputWord(word_output);
            if (countOfInputWordGivenOutputWord.ContainsKey(word_input))
            {
                return countOfInputWordGivenOutputWord[word_input];
            }
            return 0;
        }

        public void SetCountOfInputWordAndOutputWord(string word_input, string word_output, double count)
        {
            Dictionary<string, double> countOfInputWordGivenOutputWord = GetCountOfInputWordGivenOutputWord(word_output);
            countOfInputWordGivenOutputWord[word_input] = count;
        }

        protected int CreateHash(string word_input, string word_output)
        {
            int Q = 3001;
            int hash = word_input.GetHashCode();
            hash = Q * hash + word_output.GetHashCode();
            return hash;
        }

        public void UpdateProbabilityInputWordGivenOutputWord(string word_input, string word_output)
        {
            double count_input_output = GetCountOfInputWordAndOutputWord(word_input, word_output);
            double count_output = GetCountOfOutputWord(word_output);

            int hash = CreateHash(word_input, word_output);
            if (count_output == 0)
            {
                mProbabilityInputWordGivenOutputWord[hash] = 0;
            }
            else
            {
                mProbabilityInputWordGivenOutputWord[hash] = (double)count_input_output / count_output;
            }
        }

        public virtual double GetProbabilityInputWordGivenOutputWord(string word_input, string word_output)
        {
            int hash = CreateHash(word_input, word_output);
            if (mProbabilityInputWordGivenOutputWord.ContainsKey(hash))
            {
                return mProbabilityInputWordGivenOutputWord[hash];
            }
            return 0;
        }

        public virtual void SetProbabilityInputWordGivenOutputWord(string word_input, string word_output, double prob)
        {
            int hash = CreateHash(word_input, word_output);
            mProbabilityInputWordGivenOutputWord[hash] = prob;
        }

    }
}
