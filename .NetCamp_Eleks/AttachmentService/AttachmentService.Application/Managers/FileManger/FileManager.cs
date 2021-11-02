using AttachmentService.Application.Managers.FileManger.Params;
using AttachmentService.Application.Managers.FileManger.Result;
using AttachmentService.Infrastructure.HttpServices.Blog;
using External.Options.AttachmentService;
using External.Result.Base;
using External.Result.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttachmentService.Application.Managers.FileManger
{
    public class FileManager
    {
        private readonly IOptions<AttachmentServiceOptions> _attachmentOptions;
        private readonly BlogService _blogService;
        public FileManager(IOptions<AttachmentServiceOptions> attachmentOptions, BlogService blogService)
        {
            _attachmentOptions = attachmentOptions;
            _blogService = blogService;
        }

        private string SaveFile(IFormFile file)
        {
            var folderName = Path.Combine(_attachmentOptions.Value.RootPath, _attachmentOptions.Value.FolderName);
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            var ext = Path.GetExtension(file.FileName);
            var fileName = Path.GetRandomFileName() + ext;
            var fullPath = Path.Combine(pathToSave, fileName);
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return fileName;
        }

        public SaveSingleImageResult SaveSingleImage(SaveSingleImageParams saveSingleImageParams)
        {
            var validationResult = ParamsValidator.Validate(saveSingleImageParams);
            if (validationResult != null)
            {
                return new SaveSingleImageResult("", 0, validationResult);
            }

            var file = saveSingleImageParams.Image;

            if (file.Length < 1)
            {
                return new SaveSingleImageResult("", 0, new Error("file can not be empty.", ErrorType.Validation));
            }

            var fileName = SaveFile(file);

            return new SaveSingleImageResult(fileName, saveSingleImageParams.Key);
        }

        public async Task<SaveUserImageResult> SaveUserImage(SaveUserImageParams saveUserImageParams)
        {
            var validationResult = ParamsValidator.Validate(saveUserImageParams);
            if (validationResult != null)
            {
                return new SaveUserImageResult(validationResult);
            }

            var file = saveUserImageParams.Image;
            var fileName = SaveFile(file);

            var httpResult = await _blogService.SaveUserImage(new External.DTOs.BlogPlatform.Models.Request.SaveUserImageRequest()
            {
                AuthResourceUserId = saveUserImageParams.AuthResourceUserId,
                Email = saveUserImageParams.Email,
                ImageName = fileName
            });
            if (!httpResult.IsSuccess)
            {
                return new SaveUserImageResult(new Error(httpResult.Error.ErrorMessages, ErrorType.Validation));
            }
            return new SaveUserImageResult();
        }

        public void SaveBlogImage() 
        {
        
        }

        public void SavePostImage() 
        {
        
        }
    }
}
