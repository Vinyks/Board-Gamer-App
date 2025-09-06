using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board_Gamer_App
{
    public class BoardGame
    {
        private string _Name;
        private bool _DeleteButtonVisible, _VoteButtonVisible;
        private Color _BorderColor;
        private bool _IsVoted;
        public BoardGame(string name, bool deleteButtonVisible, bool voteButtonVisible, Color borderColor) 
        {
            _Name = name;
            _DeleteButtonVisible = deleteButtonVisible;
            _VoteButtonVisible = voteButtonVisible;
            _BorderColor = borderColor;
            _IsVoted = true;
        }


        public string Name { get => _Name; set => _Name = value; }

        public bool DeleteButtonVisible { get => _DeleteButtonVisible; set => _DeleteButtonVisible = value; }
        public bool VoteButtonVisible { get => _VoteButtonVisible; set => _VoteButtonVisible = value; }
        public Color BorderColor { get => _BorderColor; set => _BorderColor = value; }
        public bool IsVoted { get => _IsVoted; set => _IsVoted = value; }
    }
}
