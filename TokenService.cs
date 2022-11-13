using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Utils
{
    /// <summary>
    /// Represents result of token finding with success indicator
    /// </summary>
    public struct TokenResult
    {
        private TokenResult(string token, bool success)
        {
            Token = token;
            IsSuccessful = success;
        }

        public readonly string Token;
        public readonly bool IsSuccessful;

        internal static TokenResult Success(string token) => new(token, true);
        internal static TokenResult Fail() => new(null!, false);
    }

    public static class TokenFinding
    {
        /// <summary>
        /// Find your token in specified paths
        /// <para/> Log process to specified logger
        /// </summary>
        /// <param name="configuration">Configuration where token will be finding</param>
        /// <param name="logger">Logger to log</param>
        /// <param name="keys">Possible paths to token</param>
        /// <returns>Found token with success indicator</returns>
        public static TokenResult Find(this IConfiguration configuration, ILogger? logger, params string[] keys)
        {
            string? token = null;

            foreach (var key in keys)
                if (TryGetToken(key))
                    break;

            if (token != null) return TokenResult.Success(token);

            logger?.LogWarning("Can't get token from keys");
            return TokenResult.Fail();


            bool TryGetToken(string key)
            {
                token = configuration[key];
                if (token == null) return false;
                
                logger?.LogDebug("Get token from {Key}", key);
                return true;
            }
        }

        public static string GetTokenOrThrow(this TokenResult result)
        {
            if (result.IsSuccessful) return result.Token;

            throw new InvalidOperationException("Token wasn't get successful");
        }
    }
}