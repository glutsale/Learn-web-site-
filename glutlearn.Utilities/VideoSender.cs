using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace glutlearn.Utilities
{
    public class VideoSender
    {
        private YouTubeService _youtubeService;

        public VideoSender()
        {
            // Инициализация сервиса YouTube
            UserCredential credential;
            using (var stream = new FileStream("client_secrets.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    new[] { YouTubeService.Scope.YoutubeUpload },
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }

            _youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "GlutLearn"
            });
        }

        public string UploadVideoAndGetUrl(string videoPath, string title, string description, string[] tags)
        {
            try
            {
                var video = new Google.Apis.YouTube.v3.Data.Video();
                video.Snippet = new Google.Apis.YouTube.v3.Data.VideoSnippet();
                video.Snippet.Title = title;
                video.Snippet.Description = description;
                video.Snippet.Tags = tags;
                video.Snippet.CategoryId = "22";
                video.Status = new Google.Apis.YouTube.v3.Data.VideoStatus();
                video.Status.PrivacyStatus = "public";

                using (var fileStream = new FileStream(videoPath, FileMode.Open))
                {
                    var videosInsertRequest = _youtubeService.Videos.Insert(video, "snippet,status", fileStream, "video/*");
                    videosInsertRequest.ProgressChanged += VideosInsertRequest_ProgressChanged;
                    string videoId = null;
                    videosInsertRequest.ResponseReceived += (videoResponse) =>
                    {
                        videoId = videoResponse.Id;
                        VideosInsertRequest_ResponseReceived(videoResponse, out videoId);
                    };

                    // Загрузка видео
                    var uploadResponse = videosInsertRequest.Upload();
                    if (uploadResponse.Status == UploadStatus.Completed)
                    {
                        return "https://www.youtube.com/watch?v=" + videoId;
                    }
                    else
                    {
                        Console.WriteLine("Ошибка загрузки видео: " + uploadResponse.Exception.Message);
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
                return null;
            }
        }

        private void VideosInsertRequest_ProgressChanged(Google.Apis.Upload.IUploadProgress progress)
        {
            Console.WriteLine("Прогресс загрузки: " + progress.Status + ", " + progress.BytesSent + " байт");
        }

        private void VideosInsertRequest_ResponseReceived(Google.Apis.YouTube.v3.Data.Video videoResponse, out string videoId)
        {
            Console.WriteLine("Видео успешно загружено. ID: " + videoResponse.Id);
            videoId = videoResponse.Id;
        }
    }
}
