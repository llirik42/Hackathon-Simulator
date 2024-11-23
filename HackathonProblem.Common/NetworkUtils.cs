using System.Net.Http.Json;
using HackathonProblem.Common.exceptions;
using Newtonsoft.Json;

namespace HackathonProblem.Common;

public static class NetworkUtils
{
    public static async Task<TV> PostForEntity<T, TV>(HttpClient httpClient, string requestUri, T value)
    {
        using var response = await httpClient.PostAsJsonAsync(requestUri, value);

        if (!response.IsSuccessStatusCode) throw new UnexpectedResponseStatusException(response.StatusCode.ToString());

        var responseString = await response.Content.ReadAsStringAsync();
        var detailResponse = JsonConvert.DeserializeObject<TV>(responseString);

        if (detailResponse is null) throw new UnexpectedResponseException(responseString);

        return detailResponse;
    }
}
