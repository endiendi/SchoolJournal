namespace SchoolJournal
{
    public class Statistics
    {
        public float Max { get; private set; }
        public float Min { get; private set; }
        public float Sum { get; private set; }
        public int Count { get; private set; }
        public float ConfirmedRratingt { get; private set; }
        public float Average
        {
            get
            {
                if (Count == 0)
                {
                    return 0;
                }
                else
                {
                    return this.Sum / this.Count;
                }
            }
        }
        public char AverageLetter
        {
            get
            {
                switch (this.Average)
                {
                    case var average when average >= 6:
                        return 'A';
                    case var average when average >= 5:
                        return 'B';
                    case var average when average >= 4:
                        return 'C';
                    case var average when average >= 3:
                        return 'D';
                    case var average when average >= 2:
                        return 'E';
                    default:
                        return 'F';
                }
            }
        }
        public string? PointsCollected { get; private set; }
        public Statistics()
        {
            this.Count = 0;
            this.Sum = 0;
            this.Max = float.MinValue;
            this.Min = float.MaxValue;
            this.ConfirmedRratingt = 0;
        }
        public void AddGrade(float grade)
        {
            this.Count++;
            this.Sum += grade;
            this.Min = Math.Min(grade, this.Min);
            this.Max = Math.Max(grade, this.Max);
            this.ConfirmedRratingt = grade;

        }
    }
}
