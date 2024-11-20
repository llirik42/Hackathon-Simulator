using System.Net.Http.Json;
using HackathonProblem.Common.domain.entities;
using HackathonProblem.Common.exceptions;
using HackathonProblem.Common.models;
using Newtonsoft.Json;

namespace HackathonProblem.Developer.services;

public class HrManagerService(IHttpClientFactory factory, HrManagerConfig config) : IHrManagerService
{
    public async Task<DetailResponse> PostWishlist(Wishlist wishlist)
    {
        var httpClient = factory.CreateClient();
        var requestUri = $"{config.ConnectionString}/juniors";

        using var response = await httpClient.PostAsJsonAsync(requestUri, wishlist);

        if (!response.IsSuccessStatusCode) throw new UnexpectedResponseStatusException(response.StatusCode.ToString());

        var responseString = await response.Content.ReadAsStringAsync();
        var detailResponse = JsonConvert.DeserializeObject<DetailResponse>(responseString);

        if (detailResponse is null) throw new UnexpectedResponseException(responseString);

        return detailResponse;
    }
}
