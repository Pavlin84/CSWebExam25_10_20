
namespace Git.ViewModels.Repositores
{
    public class AllRepositoresVewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string  CreatedOn { get; set; }

        public string Owner { get; set; }

        public int ComitsCount { get; set; }

        public bool IsPublic { get; set; }

    }
}
