using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.CodeDom.Compiler;
using Microsoft.CSharp;


namespace bridge
{
    public class Player
    {
        public List<System.Windows.Controls.Image> cardplace = new List<System.Windows.Controls.Image>();
        public List<Card> cards = new List<Card>();
        public Information inf = new Information();
        public Distribution dist = new Distribution();
        public string index;

       


        public void Rearrange()
        {
            cards.Sort(Card.Compare_Cards);
            for (int i = 0; i < 13; i++)
            {
                this.cardplace[12 - i].Source = this.cards[i].image;
            }
        }

        public void Hide()
        {
            for (int i = 0; i < 13; i++)
            {
                this.cardplace[i].Source = this.cards[i].reverse_image;
            }
        }

        public void Set_Hand()
        {
            Distribution tmp = new Distribution();
            for (int i = 0; i < 13; i++)
            {
                tmp.HP += cards[i].HP;
                if (cards[i].color == 0)
                    tmp.C++;
                if (cards[i].color == 1)
                    tmp.D++;
                if (cards[i].color == 2)
                    tmp.H++;
                if (cards[i].color == 3)
                    tmp.S++;
            }

            dist = tmp;
        }

        public void Evaluate_Hand()
        {

        }

    }
}
