﻿namespace WorkOS
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// A service that offers methods to interact with WorkOS SSO.
    /// </summary>
    public class SSOService : Service
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSOService"/> class.
        /// </summary>
        public SSOService()
            : base(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SSOService"/> class.
        /// </summary>
        /// <param name="client">A client used to make requests to WorkOS.</param>
        public SSOService(WorkOSClient client)
            : base(client)
        {
        }

        /// <summary>
        /// Generates an Authorization URL to initiate the WorkOS OAuth 2.0 flow.
        /// </summary>
        /// <param name="options">Parameters used to generate the URL.</param>
        /// <returns>An Authorization URL.</returns>
        public string GetAuthorizationURL(GetAuthorizationURLOptions options)
        {
            if (options.Domain == null && options.Provider == null && options.Connection == null)
            {
                throw new ArgumentNullException("Incomplete arguments. Need to specify either a 'connection', 'domain' or 'provider'.");
            }

            var query = RequestUtilities.CreateQueryString(options);
            return $"{this.Client.ApiBaseURL}/sso/authorize?{query}";
        }

        /// <summary>
        /// Retrieves a <see cref="Profile"/> for an authenticated User.
        /// </summary>
        /// <param name="options">Options to fetch an authenticated User.</param>
        /// <param name="cancellationToken">
        /// An optional token to cancel the request.
        /// </param>
        /// <returns>A WorkOS Profile record.</returns>
        public async Task<GetProfileResponse> GetProfile(
            GetProfileOptions options,
            CancellationToken cancellationToken = default)
        {
            options.ClientSecret = this.Client.ApiKey;
            var request = new WorkOSRequest
            {
                IsJsonContentType = false,
                Options = options,
                Method = HttpMethod.Post,
                Path = "/sso/token",
            };
            return await this.Client.MakeAPIRequest<GetProfileResponse>(request, cancellationToken);
        }

        /// <summary>
        /// Activates a WorkOS Draft Connection.
        /// </summary>
        /// <param name="options">Options to activate a Draft Connection.</param>
        /// <param name="cancellationToken">
        /// An optional token to cancel the request.
        /// </param>
        /// <returns>A WorkOS Connection record.</returns>
        public async Task<Connection> CreateConnection(
            CreateConnectionOptions options,
            CancellationToken cancellationToken = default)
        {
            var request = new WorkOSRequest
            {
                Options = options,
                Method = HttpMethod.Post,
                Path = "/connections",
            };
            return await this.Client.MakeAPIRequest<Connection>(request, cancellationToken);
        }

        /// <summary>
        /// Fetches a list of Connections.
        /// </summary>
        /// <param name="options">Filter options when searching for Connections.</param>
        /// <param name="cancellationToken">
        /// An optional token to cancel the request.
        /// </param>
        /// <returns>A paginated list of Connections.</returns>
        public async Task<WorkOSList<Connection>> ListConnections(
            ListConnectionsOptions options = null,
            CancellationToken cancellationToken = default)
        {
            var request = new WorkOSRequest
            {
                Options = options,
                Method = HttpMethod.Get,
                Path = "/connections",
            };

            return await this.Client.MakeAPIRequest<WorkOSList<Connection>>(request, cancellationToken);
        }

        /// <summary>
        /// Gets a Connection.
        /// </summary>
        /// <param name="id">Connection unique identifier.</param>
        /// <param name="cancellationToken">
        /// An optional token to cancel the request.
        /// </param>
        /// <returns>A WorkOS Connection record.</returns>
        public async Task<Connection> GetConnection(string id, CancellationToken cancellationToken = default)
        {
            var request = new WorkOSRequest
            {
                Method = HttpMethod.Get,
                Path = $"/connections/{id}",
            };

            return await this.Client.MakeAPIRequest<Connection>(request, cancellationToken);
        }

        /// <summary>
        /// Deletes a Connection.
        /// </summary>
        /// <param name="id">Connection unique identifier.</param>
        /// <param name="cancellationToken">
        /// An optional token to cancel the request.
        /// </param>
        /// <returns>A deleted WorkOS Connection record.</returns>
        public async Task<Connection> DeleteConnection(string id, CancellationToken cancellationToken = default)
        {
            var request = new WorkOSRequest
            {
                Method = HttpMethod.Delete,
                Path = $"/connections/{id}",
            };

            return await this.Client.MakeAPIRequest<Connection>(request, cancellationToken);
        }
    }
}
