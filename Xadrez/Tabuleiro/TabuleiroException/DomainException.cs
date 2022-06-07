using System;
using System.Collections.Generic;
using System.Text;

namespace Xadrez.tabuleiro.TabuleiroException
{
    class DomainException : Exception
    {
        public DomainException (string msg) : base(msg)
        {

        }
    }
}
