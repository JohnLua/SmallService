using SmallService.Controllers;
using SmallService.Service;
using System.Linq;
using Xunit;

namespace SmallService.Tests
{
    public class LeaderboardControllerTest
    {
        [Fact]
        public void Rank_Normal_ReturnsRankOrderByScoreDescending()
        {
            var customerScoreService = new MemoryCustomerScoreService();
            var rankService = new MemoryRankService(customerScoreService);
            var leaderBoardService = new MemoryLeaderBoardService(customerScoreService, rankService);
            var customerController = new CustomerController(leaderBoardService);
            var leaderboardController = new LeaderboardController(leaderBoardService);

            customerController.Update(1000, 10);
            customerController.Update(1001, 11);
            customerController.Update(1001, 11);
            customerController.Update(1002, 12);
            customerController.Update(1002, 12);
            customerController.Update(1003, 13);
            customerController.Update(1004, 14);
            customerController.Update(1005, 15);

            var results = leaderboardController.Rank(1, 3).ToList();

            Assert.Equal(3, results.Count);
            Assert.Equal(1, results[0].Rank);
            Assert.Equal(1002, results[0].CustomerId);
            Assert.Equal(24, results[0].Score);
            Assert.Equal(2, results[1].Rank);
            Assert.Equal(1001, results[1].CustomerId);
            Assert.Equal(22, results[1].Score);
            Assert.Equal(3, results[2].Rank);
            Assert.Equal(1005, results[2].CustomerId);
            Assert.Equal(15, results[2].Score);
        }

        [Fact]
        public void Rank_EndValueExceeded_ReturnsRankWithValidEndValue()
        {
            var customerScoreService = new MemoryCustomerScoreService();
            var rankService = new MemoryRankService(customerScoreService);
            var leaderBoardService = new MemoryLeaderBoardService(customerScoreService, rankService);
            var customerController = new CustomerController(leaderBoardService);
            var leaderboardController = new LeaderboardController(leaderBoardService);

            customerController.Update(1000, 10);
            customerController.Update(1001, 11);
            customerController.Update(1002, 12);
            customerController.Update(1003, 13);
            customerController.Update(1004, 14);
            customerController.Update(1005, 15);

            var results = leaderboardController.Rank(5, 10).ToList();

            Assert.Equal(2, results.Count);
            Assert.Equal(1, results[0].Rank);
            Assert.Equal(1001, results[0].CustomerId);
            Assert.Equal(11, results[0].Score);
            Assert.Equal(2, results[1].Rank);
            Assert.Equal(1000, results[1].CustomerId);
            Assert.Equal(10, results[1].Score);
        }

        [Fact]
        public void Rank_RangeNotExist_ReturnsEmpty()
        {
            var customerScoreService = new MemoryCustomerScoreService();
            var rankService = new MemoryRankService(customerScoreService);
            var leaderBoardService = new MemoryLeaderBoardService(customerScoreService, rankService);
            var customerController = new CustomerController(leaderBoardService);
            var leaderboardController = new LeaderboardController(leaderBoardService);

            customerController.Update(1000, 10);
            customerController.Update(1001, 11);
            customerController.Update(1002, 12);
            customerController.Update(1003, 13);
            customerController.Update(1004, 14);
            customerController.Update(1005, 15);

            var results = leaderboardController.Rank(10, 20);

            Assert.Empty(results);
        }

        [Fact]
        public void Rank_CustomerWithSameScore_ReturnsRankOrderByCustomerIdAscending()
        {
            var customerScoreService = new MemoryCustomerScoreService();
            var rankService = new MemoryRankService(customerScoreService);
            var leaderBoardService = new MemoryLeaderBoardService(customerScoreService, rankService);
            var customerController = new CustomerController(leaderBoardService);
            var leaderboardController = new LeaderboardController(leaderBoardService);

            customerController.Update(1000, 99);
            customerController.Update(2000, 99);
            customerController.Update(1500, 99);

            var results = leaderboardController.Rank(1, 3).ToList();

            Assert.Equal(3, results.Count);
            Assert.Equal(1, results[0].Rank);
            Assert.Equal(1000, results[0].CustomerId);
            Assert.Equal(99, results[0].Score);
            Assert.Equal(2, results[1].Rank);
            Assert.Equal(1500, results[1].CustomerId);
            Assert.Equal(99, results[1].Score);
            Assert.Equal(3, results[2].Rank);
            Assert.Equal(2000, results[2].CustomerId);
            Assert.Equal(99, results[2].Score);
        }

        [Fact]
        public void Rank_CustomerWithZeroScore_ReturnsRankMissZeroScoreCustomers()
        {
            var customerScoreService = new MemoryCustomerScoreService();
            var rankService = new MemoryRankService(customerScoreService);
            var leaderBoardService = new MemoryLeaderBoardService(customerScoreService, rankService);
            var customerController = new CustomerController(leaderBoardService);
            var leaderboardController = new LeaderboardController(leaderBoardService);

            customerController.Update(1000, 0);
            customerController.Update(1001, 0);
            customerController.Update(1002, 12);
            customerController.Update(1003, 0);
            customerController.Update(1004, 14);
            customerController.Update(1005, 0);

            var results = leaderboardController.Rank(1, 3).ToList();

            Assert.Equal(2, results.Count);
            Assert.Equal(1, results[0].Rank);
            Assert.Equal(1004, results[0].CustomerId);
            Assert.Equal(14, results[0].Score);
            Assert.Equal(2, results[1].Rank);
            Assert.Equal(1002, results[1].CustomerId);
            Assert.Equal(12, results[1].Score);
        }

        [Fact]
        public void Rank_Normal_ReturnsRankByCustomerId()
        {
            var customerScoreService = new MemoryCustomerScoreService();
            var rankService = new MemoryRankService(customerScoreService);
            var leaderBoardService = new MemoryLeaderBoardService(customerScoreService, rankService);
            var customerController = new CustomerController(leaderBoardService);
            var leaderboardController = new LeaderboardController(leaderBoardService);

            customerController.Update(1000, 10);
            customerController.Update(1001, 11);
            customerController.Update(1002, 12);
            customerController.Update(1003, 13);
            customerController.Update(1004, 14);
            customerController.Update(1005, 15);

            var results = leaderboardController.RankByCustomerId(1002, 1, 1).ToList();

            Assert.Equal(3, results.Count);
            Assert.Equal(1, results[0].Rank);
            Assert.Equal(1003, results[0].CustomerId);
            Assert.Equal(13, results[0].Score);
            Assert.Equal(2, results[1].Rank);
            Assert.Equal(1002, results[1].CustomerId);
            Assert.Equal(12, results[1].Score);
            Assert.Equal(3, results[2].Rank);
            Assert.Equal(1001, results[2].CustomerId);
            Assert.Equal(11, results[2].Score);
        }

        [Fact]
        public void Rank_CustomerWithZeroScore_ReturnsEmptyByCustomerId()
        {
            var customerScoreService = new MemoryCustomerScoreService();
            var rankService = new MemoryRankService(customerScoreService);
            var leaderBoardService = new MemoryLeaderBoardService(customerScoreService, rankService);
            var customerController = new CustomerController(leaderBoardService);
            var leaderboardController = new LeaderboardController(leaderBoardService);

            customerController.Update(1000, 0);
            customerController.Update(1001, 0);
            customerController.Update(1002, 12);
            customerController.Update(1003, 0);
            customerController.Update(1004, 14);
            customerController.Update(1005, 0);

            var results = leaderboardController.RankByCustomerId(1003, 1, 3);

            Assert.Empty(results);
        }

        [Fact]
        public void Rank_HighValueExceeded_ReturnsRankWithValidHighValueByCustomerId()
        {
            var customerScoreService = new MemoryCustomerScoreService();
            var rankService = new MemoryRankService(customerScoreService);
            var leaderBoardService = new MemoryLeaderBoardService(customerScoreService, rankService);
            var customerController = new CustomerController(leaderBoardService);
            var leaderboardController = new LeaderboardController(leaderBoardService);

            customerController.Update(1000, 10);
            customerController.Update(1001, 11);
            customerController.Update(1002, 12);
            customerController.Update(1003, 13);
            customerController.Update(1004, 14);
            customerController.Update(1005, 15);

            var results = leaderboardController.RankByCustomerId(1004, 3, 3).ToList();

            Assert.Equal(5, results.Count);
            Assert.Equal(1, results[0].Rank);
            Assert.Equal(1005, results[0].CustomerId);
            Assert.Equal(15, results[0].Score);
            Assert.Equal(2, results[1].Rank);
            Assert.Equal(1004, results[1].CustomerId);
            Assert.Equal(14, results[1].Score);
            Assert.Equal(3, results[2].Rank);
            Assert.Equal(1003, results[2].CustomerId);
            Assert.Equal(13, results[2].Score); 
            Assert.Equal(4, results[3].Rank);
            Assert.Equal(1002, results[3].CustomerId);
            Assert.Equal(12, results[3].Score); 
            Assert.Equal(5, results[4].Rank);
            Assert.Equal(1001, results[4].CustomerId);
            Assert.Equal(11, results[4].Score);
        }

        [Fact]
        public void Rank_LowValueExceeded_ReturnsRankWithValidLowValueByCustomerId()
        {
            var customerScoreService = new MemoryCustomerScoreService();
            var rankService = new MemoryRankService(customerScoreService);
            var leaderBoardService = new MemoryLeaderBoardService(customerScoreService, rankService);
            var customerController = new CustomerController(leaderBoardService);
            var leaderboardController = new LeaderboardController(leaderBoardService);

            customerController.Update(1000, 10);
            customerController.Update(1001, 11);
            customerController.Update(1002, 12);
            customerController.Update(1003, 13);
            customerController.Update(1004, 14);
            customerController.Update(1005, 15);

            var results = leaderboardController.RankByCustomerId(1001, 3, 3).ToList();

            Assert.Equal(5, results.Count);
            Assert.Equal(1, results[0].Rank);
            Assert.Equal(1004, results[0].CustomerId);
            Assert.Equal(14, results[0].Score);
            Assert.Equal(2, results[1].Rank);
            Assert.Equal(1003, results[1].CustomerId);
            Assert.Equal(13, results[1].Score);
            Assert.Equal(3, results[2].Rank);
            Assert.Equal(1002, results[2].CustomerId);
            Assert.Equal(12, results[2].Score);
            Assert.Equal(4, results[3].Rank);
            Assert.Equal(1001, results[3].CustomerId);
            Assert.Equal(11, results[3].Score);
            Assert.Equal(5, results[4].Rank);
            Assert.Equal(1000, results[4].CustomerId);
            Assert.Equal(10, results[4].Score);
        }
    }
}
