using System.ComponentModel.DataAnnotations;

namespace KeyLogger2.Models
{
    public class Home
    {
        [Key]
        public int ForumId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
    }

    public class HomeViewModel
    {
        public HomeViewModel(List<Home> homes, int lastPage, int currPage)
        {
            Homes = homes;
            LastPage = lastPage;
            CurrPage = currPage;
        }

        public List<Home> Homes { get; set; }

        public int LastPage { get; set; }

        public int CurrPage { get; set; }
    }
}

