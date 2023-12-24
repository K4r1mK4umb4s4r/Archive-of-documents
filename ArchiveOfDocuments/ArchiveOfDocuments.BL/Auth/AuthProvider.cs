using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Duende.IdentityServer.Models;
using IdentityModel.Client;
using Microsoft.AspNetCore.Identity;
using ArchiveOfDocuments.BL.Auth.Entities;
using ArchiveOfDocuments.BL.Auth.Exeptions;
using ArchiveOfDocuments.BL.Auth;
using ArchiveOfDocuments.DataAccess.Entities;
using System.Net.Http;

namespace ArchiveOfDocuments.BL.Auth
{
    public class AuthProvider : IAuthProvider
    {
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly UserManager<UserEntity> _userManager;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _identityServerUri;
        private readonly string _clientId;
        private readonly string _clientSecret;

        public AuthProvider(SignInManager<UserEntity> signInManager, UserManager<UserEntity> userManager,
            IHttpClientFactory httpClientFactory,
            string identityServerUri,
            string clientId,
            string clientSecret)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _identityServerUri = identityServerUri;
            _httpClientFactory = httpClientFactory;
            _clientId = clientId;
            _clientSecret = clientSecret;
        }

        public async Task<TokensResponse> AuthorizeUser(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email); //IRepository<UserEntity> get user by email
            if (user is null)
            {
                throw new UserNotFoundException(); //UserNotFoundException, BusinessLogicException(Code.UserNotFound);
            }

            var verificationPasswordResult = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (!verificationPasswordResult.Succeeded)
            {
                throw new AuthorizationException(); //AuthorizationException, BusinessLogicException(Code.PasswordOrLoginIsIncorrect);
            }

            var client = _httpClientFactory.CreateClient();
            var discoveryDoc = await client.GetDiscoveryDocumentAsync(_identityServerUri); //
            if (discoveryDoc.IsError)
            {
                throw new AuthorizationException();
            }

            var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest()
            {
                Address = discoveryDoc.TokenEndpoint,
                GrantType = GrantType.ResourceOwnerPassword,
                ClientId = _clientId,
                ClientSecret = _clientSecret,
                UserName = user.UserName,
                Password = password,
                Scope = "api offline_access"
            });

            if (tokenResponse.IsError)
            {
                throw new AuthorizationException();
            }

            return new TokensResponse()
            {
                AccessToken = tokenResponse.AccessToken,
                RefreshToken = tokenResponse.RefreshToken
            };
        }

        public async Task RegisterUser(string email, string password)
        {
            UserEntity userEntity = new UserEntity()
            {
                Email = email,
                UserName = email
            };

            var createUserResult = await _userManager.CreateAsync(userEntity, password);
            if (!createUserResult.Succeeded)
            {
                throw new Exception();
            }
        }
    }
}