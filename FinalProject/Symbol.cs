using System;
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
                _ => null
            };
            lastSavedSymbolType = symbolType;
        }
    }
}