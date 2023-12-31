using SmallService.Controllers;
using SmallService.Service;
using Xunit;

namespace SmallService.Tests
{
    public class CustomerControllerTest
    {
        [Fact]
        public void Update_NewCustomerWithDefaultScore_ReturnsZeroScore()
        {
            var customerScoreService = new MemoryCustomerScoreService();
            var rankService = new MemoryRankService(customerScoreService);
            var leaderBoardService = new MemoryLeaderBoardService(customerScoreService, rankService);
            var controller = new CustomerController(leaderBoardService);

            var result = controller.Update(1001);

            Assert.Equal(0, result);
        }

        [Fact]
        public void Update_CustomerUpdateMultipleScore_ReturnsSumScore()
        {
            var customerScoreService = new MemoryCustomerScoreService();
            var rankService = new MemoryRankService(customerScoreService);
            var leaderBoardService = new MemoryLeaderBoardService(customerScoreService, rankService);
            var controller = new CustomerController(leaderBoardService);
            
            controller.Update(1001, 9.9m);
            controller.Update(1001, -8.8m);
            var result = controller.Update(1001, 7.7m);

            Assert.Equal(8.8m, result);
        }

        [Fact]
        public void Update_CustomerWithMaxScoreExceeded_ReturnsMaxScore()
        {
            var customerScoreService = new MemoryCustomerScoreService();
            var rankService = new MemoryRankService(customerScoreService);
            var leaderBoardService = new MemoryLeaderBoardService(customerScoreService, rankService);
            var controller = new CustomerController(leaderBoardService);

            controller.Update(1001, 777m);
            var result = controller.Update(1001, 777m);

            Assert.Equal(1000m, result);
        }

        [Fact]
        public void Update_CustomerWithMinScoreExceeded_ReturnsMinScore()
        {
            var customerScoreService = new MemoryCustomerScoreService();
            var rankService = new MemoryRankService(customerScoreService);
            var leaderBoardService = new MemoryLeaderBoardService(customerScoreService, rankService);
            var controller = new CustomerController(leaderBoardService);

            controller.Update(1001, -777m);
            var result = controller.Update(1001, -777m);

            Assert.Equal(-1000m, result);
        }
    }
}
