using Google.Apis.Drive.v3;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ZipContent.Core;

namespace ZipContent.GoogleDrive
{
    public class GoogleDrivePartialFileReader : IPartialFileReader
    {
        private readonly string _fileId;
        private readonly FilesResource.GetRequest _getRequest;

        public GoogleDrivePartialFileReader(DriveService driveService, string fileId)
        {
            _fileId = fileId;
            _getRequest = driveService.Files.Get(_fileId);
            _getRequest.Fields = "size";
        }

        public async Task<long> ContentLength()
        {
            var response = await _getRequest.ExecuteAsync();
            return response.Size.GetValueOrDefault();
        }

        public async Task<byte[]> GetBytes(ByteRange range)
        {
            var stream = new System.IO.MemoryStream();
            await _getRequest.DownloadRangeAsync(stream, new RangeHeaderValue(range.Start, range.End));
            return stream.ToArray();
        }
    }
}
