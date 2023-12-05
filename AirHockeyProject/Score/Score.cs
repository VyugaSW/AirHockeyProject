using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirHockeyProject
{
    public class Score : INotifyPropertyChanged
    {
        private int _scoring = 0;
        public int Scoring 
        {
            get => _scoring;
            set
            {
                _scoring = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Scoring)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void AddScore(int additionalScore)
        {
            Scoring += additionalScore;
        }
        public void ClearScore()
        {
            Scoring = 0;
        }
    }
}
