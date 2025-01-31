﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public class Symbol : Displayable
    {
        private readonly SpriteManager spriteManager = new SpriteManager();

        public override ImageSource? ImageSource { get; set; }
        public SymbolType lastSavedSymbolType { get; set; }
        public Symbol(SymbolType symbolType = SymbolType.None)
        {
            UpdateImageSource(symbolType);
        }

        private void UpdateImageSource(SymbolType symbolType)
        {
            ImageSource = symbolType switch
            {
                SymbolType.Plus => spriteManager.GetPlus(),
                SymbolType.Multiply => spriteManager.GetMultiply(),
                SymbolType.Division => spriteManager.GetDivision(),
                SymbolType.Minus => spriteManager.GetMinus(),
                _ => null
            };
            lastSavedSymbolType = symbolType;
        }

        public override bool Compare(Displayable d)
        {
            if (d is Symbol symbol)
            {
                if (symbol.lastSavedSymbolType == lastSavedSymbolType) return true;
            }
            return false;
        }
    }
}