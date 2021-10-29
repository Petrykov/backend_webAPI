using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend_dockerAPI.Controllers 
{
 [Route("api/[controller]")]
 public class ImageController: ControllerBase {
  public static IWebHostEnvironment _environment;
  public ImageController(IWebHostEnvironment environment) {
   _environment = environment;
  }
  public class FIleUploadAPI {
   public IFormFile files {
    get;
    set;
   }
  }
  [HttpPost]
  public async Task < string > Post(FIleUploadAPI objfile) {
      Console.WriteLine(_environment.WebRootPath);
   if (objfile.files.Length > 0) {
    try {
     if (!Directory.Exists(Path.Combine(_environment.WebRootPath, "uploads"))) {
      Directory.CreateDirectory(Path.Combine(_environment.WebRootPath, "uploads"));
     }
     using(FileStream filestream = System.IO.File.Create(Path.Combine(_environment.WebRootPath, "uploads", objfile.files.FileName))) {
      objfile.files.CopyTo(filestream);
      filestream.Flush();
      return "/uploads/" + objfile.files.FileName;
     }
    } catch (Exception ex) {
     return ex.ToString();
    }
   } else {
    return "Unsuccessful";
   }

  }
 }
} 