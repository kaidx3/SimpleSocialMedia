using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleSocialMedia_ClassLibrary.BLL;
using SimpleSocialMedia_ClassLibrary.Entities;

namespace SimpleSocialMedia_WebApp.Pages
{
    public class MainModel : PageModel
    {
        public int AccountID { get; set; }

        public string LoginToken { get; set; }

        public Account Login { get; set; }

        [BindProperty]
        public int PostID { get; set; }

        [BindProperty]
        public int ViewCommentPost { get; set; } = -1;

        public List<Comment> Comments { get; set; }

        private readonly AccountServices _accountServices;

        private readonly PostServices _postServices;

        private readonly CommentServices _commentServices;

        [BindProperty]
        public string CommentText { get; set; }

        public List<Post> Posts;

        public MainModel(AccountServices accountServices, PostServices postServices, CommentServices commentServices)
        {
            _accountServices = accountServices;
            _postServices = postServices;
            _commentServices = commentServices;
        }

        public void OnGet()
        {
            Posts = _postServices.GetPosts_All();
            LoginToken = Request.Cookies["LoginToken"];
            Login = _accountServices.GetAccount_Token(LoginToken);
            AccountID = Login.AccountID;
        }

        public IActionResult OnPostLikePost()
        {
            _postServices.LikePost(PostID);
            return RedirectToPage();
        }

        public IActionResult OnPostComment()
        {
            if (!string.IsNullOrWhiteSpace(CommentText))
            {
                Comment comment = new();
                comment.Text = CommentText;
                comment.AccountID = AccountID;
                comment.PostID = PostID;
                comment.Likes = 0;
                _commentServices.CreateComment(comment);
            }
            return RedirectToPage();
        }

        public IActionResult OnPostViewComment()
        {
            ViewCommentPost = PostID;
            Comments = _commentServices.GetComment_Post(PostID);
            Posts = _postServices.GetPosts_All();
            return Page();
        }
    }
}
