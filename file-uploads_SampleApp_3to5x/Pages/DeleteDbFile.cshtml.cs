using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using file_uploads_SampleApp_3to5x.Data;
using file_uploads_SampleApp_3to5x.Models;

namespace file_uploads_SampleApp_3to5x.Pages
{
    public class DeleteDbFileModel : PageModel
    {
        private readonly AppDbContext _context;

        public DeleteDbFileModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AppFile RemoveFile { get; private set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return RedirectToPage("/Index");
            }

            RemoveFile = await _context.File.SingleOrDefaultAsync(m => m.Id == id);

            if (RemoveFile == null)
            {
                return RedirectToPage("/Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return RedirectToPage("/Index");
            }

            RemoveFile = await _context.File.FindAsync(id);

            if (RemoveFile != null)
            {
                _context.File.Remove(RemoveFile);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Index");
        }
    }
}
