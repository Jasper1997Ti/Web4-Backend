namespace FitnessApi.Models
{
    public class Exercise
    {
        #region Properties
        public int Id { get; set; }

        public string Name { get; set; }

        public int Sets { get; set; }

        public int Reps { get; set; }
        #endregion

        #region Constructors
        public Exercise(string name, int sets, int reps)
        {
            Name = name;
            Sets = sets;
            Reps = reps;
        }
        #endregion
    }
}