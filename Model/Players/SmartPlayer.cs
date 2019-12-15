using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Players
{
    public class SmartPlayer : BasePlayer
    {
        private static readonly Random R = new Random();

        public string Name;
        public Color Color;

        virtual public int SmartMove(BasePiece[][,] options)
        {
            int bestIndex = 0;
            double bestScore = ScoreMove(options[0]);

            for (int i = 1; i < options.Length; i++)
            {
                double score = ScoreMove(options[i]);
                if (score > bestScore)
                {
                    bestIndex = i;
                    bestScore = score;
                }
            }

            return bestIndex;
        }

        virtual protected double SmartScore(BasePiece[,] options)
        {
            return R.NextDouble() - .5;
        }
    }
}
