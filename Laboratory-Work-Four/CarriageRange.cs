using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Laboratory_Work_Four
{
    public class CarriageRange
    {
        public Versioning From { get; private set; }

        public Versioning Before { get; private set; }

        private const string ERROR_VERSION_RANGE = "Диапазон версий не корректен!";

        private const string ERROR_EXPANDED_MODEL = "Расширенный модельный ряд не корректен!";

        private const string ERROR_CARRIAGE_RECORDING = "Не выполняется условие записи диапазона каретки!";

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
                throw new ArgumentException(ERROR_VERSION_RANGE);
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

            bool isFindSymbol = false;

            for (int i = 0; i < From.MainVersionParts.Count; i++)
            {
                if (From.MainVersionParts[i] != 0 && !isFindSymbol)
                {
                    beforeVersions.Add(From.MainVersionParts[i] + 1);

                    isFindSymbol = true;

                    continue;
                }

                beforeVersions.Add(0);
            }

            if (!isFindSymbol)
            {
                beforeVersions[0] = 1;
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
                throw new ArgumentException(ERROR_EXPANDED_MODEL);
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

                    if (beforeMainVersionParts[i] != fromVersion + 1)
                    {
                        throw new ArgumentException(ERROR_CARRIAGE_RECORDING);
                    }

                    for (int j = i + 1; j < beforeMainVersionParts.Count; j++)
                    {
                        if (beforeMainVersionParts[j] != 0)
                        {
                            throw new ArgumentException(ERROR_CARRIAGE_RECORDING);
                        }
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
            return range.From >= From && range.From < Before && range.Before >= From && range.Before <= Before;
        }
    }
}
