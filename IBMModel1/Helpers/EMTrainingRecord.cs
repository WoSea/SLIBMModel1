﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBMModel1.Helpers
{
    class EMTrainingRecord
    {
        protected string[] mInputLang;
        protected string[] mOutputLang;

        public string[] InputLang
        {
            get { return mInputLang; }
            set { mInputLang = value; }
        }

        public string[] OutputLang
        {
            get { return mOutputLang; }
            set { mOutputLang = value; }
        }
    }
}
