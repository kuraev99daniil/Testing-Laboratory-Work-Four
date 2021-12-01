using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Laboratory_Work_Four
{
    class CarriageRange
    {
        public Versioning From { get; private set; }

        public Versioning Before { get; private set; }

        public CarriageRange(string carriageRange, bool isExpanded)
        {
            CheckRange(carriageRange, isExpanded);            
        }

        private void CheckRange(string carriageRange, bool isExpanded)
        {
            if (isExpanded)
            {
                ParseExtendedModelRange(carriageRange);
            }
            else
            {
                ParseVersionRange(carriageRange);
            }
        }

        private void ParseVersionRange(string versionRange)
        {
            bool isCorrect = Regex.IsMatch(versionRange, @"\^\d+(\.[x|\d+])*");

            if (!isCorrect)
            {
                throw new ArgumentException("Диапазон версий не корректен!");
            }

            var fromVersions = versionRange.Replace("^", "").Split(".").ToList();

            var countEmptySymbols = 3 - fromVersions.Count;

            for (int i = 0; i < countEmptySymbols; i++)
            {
                fromVersions.Add("0");
            }

            for (int i = 0; i < fromVersions.Count; i++)
            {
                if (fromVersions[i] == "x")
                {
                    fromVersions[i] = "0";
                }
            }

            string fromVersion = fromVersions[0];

            for (int i = 1; i < fromVersions.Count; i++)
            {
                fromVersion += $".{fromVersions[i]}";
            }

            From = new Versioning(fromVersion);

            List<int> beforeVersions = new List<int>();

            for (int i = 0; i < From.MainVersionParts.Count; i++)
            {
                if (From.MainVersionParts[i] != 0)
                {
                    beforeVersions.Add(From.MainVersionParts[i] + 1);

                    continue;
                }

                beforeVersions.Add(From.MainVersionParts[i]);
            }

            string beforeVersion = $"{beforeVersions[0]}";

            for (int i = 1; i < beforeVersions.Count; i++)
            {
                beforeVersion += $".{beforeVersions[i]}";
            }

            Before = new Versioning(beforeVersion);
        }

        private void ParseExtendedModelRange(string extendedModelRange)
        {
            bool isCorrect = Regex.IsMatch(extendedModelRange, @">=\d+\.\d+\.\d+ <\d+\.\d+\.\d+");

            if (!isCorrect)
            {
                throw new ArgumentException("Расширенный модельный ряд не корректен!");
            }

            var versions = extendedModelRange.Replace(">=", "").Replace("<", "").Split(" ");

            From = new Versioning(versions[0]);

            Before = new Versioning(versions[1]);

            var fromMainVersionParts = From.MainVersionParts;
            for (int i = 0; i < fromMainVersionParts.Count; i++)
            {
                var fromVersion = fromMainVersionParts[i];

                if (fromVersion != 0)
                {
                    var beforeMainVersionParts = Before.MainVersionParts;

                    if (beforeMainVersionParts[i] != fromVersion + 1) throw new ArgumentException("Не выполняется условие записи диапазона каретки!");

                    for (int j = i + 1; j < beforeMainVersionParts.Count; j++)
                    {
                        if (beforeMainVersionParts[j] != 0) throw new ArgumentException("Не выполняется условие записи диапазона каретки!");
                    }

                    return;
                }
            }
        }

        public override string ToString()
        {
            return $">={From} <{Before}";
        }

        public bool Contains(Versioning versioning)
        {
            return versioning >= From && versioning < Before;
        }

        public bool Contains(CarriageRange range)
        {
            return range.From >= From && range.From < Before && range.Before >= From && range.Before < Before;
        }
    }
}
