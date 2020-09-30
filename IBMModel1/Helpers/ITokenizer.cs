using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMModel1.Helpers
{
    public interface ITokenizer
    {
        string[] Tokenize(string content);
        string[] Tokens
        {
            get;
            set;
        }

        ITokenizer Clone();
        void Clear();
    }
}
