using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchedule
{
    public class RandomEx : Random
    {
        public bool Next(int probability)
        {
            int value = Next(0, 101);

            if (value <= probability)
                return true;
            return false;
        }

        public T Next<T>(Dictionary<T, int> probabilityMap)
        {
            int probabilitySum = 0;
            foreach (int probability in probabilityMap.Values)
                probabilitySum += probability;

            int value = Next(0, probabilitySum);

            probabilitySum = 0;
            foreach (KeyValuePair<T, int> pair in probabilityMap)
            {
                if (value >= probabilitySum && value < probabilitySum + pair.Value)
                    return pair.Key;

                probabilitySum += pair.Value;
            }

            throw new Exception("Unable to get random item");
        }

    }
}
