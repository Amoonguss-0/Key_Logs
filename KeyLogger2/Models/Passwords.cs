using System.ComponentModel.DataAnnotations;

namespace KeyLogger2.Models
{
    public class Passwords
    {
        [Key]
        public int AccountId { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public string? UserName { get; set; }

        public string Password { get; set; }

        public int MemberId { get; set; }
    }

    public class PasswordPageViewModel
    {
        public PasswordPageViewModel(List<Passwords> passwords, int lastPage, int currPage)
        {
            Passwords = passwords;
            LastPage = lastPage;
            CurrentPage = currPage;
        }

        public List<Passwords> Passwords { get; set; }

        public int LastPage { get; private set; }


        public int CurrentPage { get; private set; }
    }
}
