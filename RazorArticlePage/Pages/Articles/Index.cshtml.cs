using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorArticlePage.Data;
using RazorArticlePage.Models;

namespace RazorArticlePage.Pages.Articles
{
    public class IndexModel : PageModel
    {
        private readonly RazorArticlePage.Data.ApplicationDbContext _context;

        public IndexModel(RazorArticlePage.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Article> Article { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Article = await _context.Articles.ToListAsync();
        }
    }
}
