// using System;
// using System.Threading;
// using System.Threading.Tasks;
// using Indextracker2.Application.Background;
// using Indextracker2.Application.Services;
// using Indextracker2.Domain.Entities;
// using Indextracker2.Domain.Repositories;
// using Microsoft.Extensions.Logging;
// using Moq;
// using Xunit;

// namespace Indextracker2.Tests
// {
//     public class IndexValueBackgroundServiceTests
//     {
//         [Fact]
//         public async Task ExecuteAsync_PrintsAndFetchesAtCorrectIntervals()
//         {
//             // Arrange
//             var sp500ServiceMock = new Mock<ISp500Service>();
//             var repositoryMock = new Mock<IIndexValueRepository>();
//             var loggerMock = new Mock<ILogger<IndexValueBackgroundService>>();

//             var testValue = new IndexValue { Timestamp = DateTime.UtcNow, Value = 5000m };
//             var fetchCount = 0;
//             var printCount = 0;

//             sp500ServiceMock.Setup(s => s.GetCurrentValueAsync(It.IsAny<CancellationToken>()))
//                 .ReturnsAsync(new Application.Models.Sp500ValueDto { Timestamp = testValue.Timestamp, Value = testValue.Value })
//                 .Callback(() => fetchCount++);

//             repositoryMock.Setup(r => r.AddAsync(It.IsAny<IndexValue>(), It.IsAny<CancellationToken>()))
//                 .Returns(Task.CompletedTask);
//             repositoryMock.Setup(r => r.GetLatestAsync(It.IsAny<CancellationToken>()))
//                 .ReturnsAsync(() => testValue)
//                 .Callback(() => printCount++);

//             var service = new IndexValueBackgroundService(
//                 sp500ServiceMock.Object,
//                 repositoryMock.Object,
//                 loggerMock.Object
//             );

//             using var cts = new CancellationTokenSource();
//             // Run for 2500ms to allow at least 2 prints and 1 fetch
//             var runTask = service.StartAsync(cts.Token);
//             await Task.Delay(2500);
//             cts.Cancel();
//             await runTask;

//             // Assert
//             Assert.True(fetchCount >= 1); // At least one fetch
//             Assert.True(printCount >= 2); // At least two prints
//         }
//     }
// }
