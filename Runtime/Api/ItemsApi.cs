// Copyright (c) 2018 - 2022 AccelByte Inc. All Rights Reserved.
// This is licensed software from AccelByte Inc, for limitations
// and restrictions contact your company contract manager.

using System;
using System.Collections;
using System.Collections.Generic;
using AccelByte.Core;
using AccelByte.Models;
using UnityEngine.Assertions;

namespace AccelByte.Api
{
    internal class ItemsApi : ApiBase
    {
        /// <summary>
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="config">baseUrl==PlatformServerUrl</param>
        /// <param name="session"></param>
        internal ItemsApi( IHttpClient httpClient
            , Config config
            , ISession session ) 
            : base( httpClient, config, config.PlatformServerUrl, session )
        {
        }

        public IEnumerator GetItem( string itemId
            , string region
            , string language
            , ResultCallback<PopulatedItemInfo> callback )
        {
            Report.GetFunctionLog(GetType().Name);
            Assert.IsNotNull(Namespace_, "Can't get item! Namespace parameter is null!");
            Assert.IsNotNull(AuthToken, "Can't get item! AccessToken parameter is null!");
            Assert.IsNotNull(itemId, "Can't get item! ItemId parameter is null!");

            var builder = HttpRequestBuilder
                .CreateGet(BaseUrl + "/public/namespaces/{namespace}/items/{itemId}/locale")
                .WithPathParam("namespace", Namespace_)
                .WithPathParam("itemId", itemId)
                .WithBearerAuth(AuthToken)
                .Accepts(MediaType.ApplicationJson);

            if(region != null)
            {
                builder.WithQueryParam("region", region);
            }

            if(language != null)
            {
                builder.WithQueryParam("language", language);
            }

            var request = builder.GetResult();

            IHttpResponse response = null;

            yield return HttpClient.SendRequest(request, 
                rsp => response = rsp);

            var result = response.TryParseJson<PopulatedItemInfo>();

            callback.Try(result);
        }

        /// <summary>
        /// </summary>
        /// <param name="publisherNamespace">Different than Config Namespace</param>
        /// <param name="appId"></param>
        /// <param name="callback"></param>
        /// <param name="language"></param>
        /// <param name="region"></param>
        /// <returns></returns>
        public IEnumerator GetItemByAppId( string publisherNamespace
            , string appId
            , ResultCallback<ItemInfo> callback
            , string language = null
            , string region = null )
        {
            Report.GetFunctionLog(GetType().Name);
            Assert.IsNotNull(publisherNamespace, "Can't get items by appId! publisherNamespace parameter is null!");
            Assert.IsNotNull(AuthToken, "Can't get items by appId! AccessToken parameter is null!");
            Assert.IsNotNull(appId, "Can't get items by appId! appId parameter is null!");

            var builder = HttpRequestBuilder
                .CreateGet(BaseUrl + "/public/namespaces/{namespace}/items/byAppId")
                .WithPathParam("namespace", publisherNamespace)
                .WithQueryParam("appId", appId)
                .Accepts(MediaType.ApplicationJson);

            if (region != null)
            {
                builder.WithQueryParam("region", region);
            }

            if (language != null)
            {
                builder.WithQueryParam("language", language);
            }

            var request = builder.GetResult();

            IHttpResponse response = null;

            yield return HttpClient.SendRequest(request, 
                rsp => response = rsp);

            var result = response.TryParseJson<ItemInfo>();

            callback.Try(result);
        }

        public IEnumerator GetItemsByCriteria( ItemCriteria criteria
            , ResultCallback<ItemPagingSlicedResult> callback )
        {
            Report.GetFunctionLog(GetType().Name);
            Assert.IsNotNull(Namespace_, "Can't get items by criteria! Namespace parameter is null!");
            Assert.IsNotNull(AuthToken, "Can't get items by criteria! AccessToken parameter is null!");
            Assert.IsNotNull(criteria, "Can't get items by criteria! Criteria parameter is null!");

            var queries = new Dictionary<string, string>();

            if (criteria != null)
            {
                if (criteria.categoryPath != null)
                {
                    queries.Add("categoryPath", criteria.categoryPath);
                }

                if (criteria.itemType != ItemType.NONE)
                {
                    queries.Add("itemType", criteria.itemType.ToString());
                }

                if (criteria.appType != EntitlementAppType.NONE)
                {
                    queries.Add("appType", criteria.appType.ToString());
                }

                if(criteria.region != null)
                {
                    queries.Add("region", criteria.region);
                }

                if(criteria.language != null)
                {
                    queries.Add("language", criteria.language);
                }

                if(criteria.tags != null)
                {
                    string tags = "";
                    for(int i=0;i<criteria.tags.Length;i++){
                        tags += (i < criteria.tags.Length - 1 ? criteria.tags[i] + "," : criteria.tags[i]);
                    }
                    queries.Add("tags", tags);
                }

                if(criteria.features != null)
                {
                    string feats = "";
                    for (int i = 0; i < criteria.features.Length; i++)
                    {
                        feats += (i < criteria.features.Length - 1 ? criteria.features[i] + "," : criteria.features[i]);
                    }
                    queries.Add("features", feats);
                }

                if (criteria.offset != null)
                {
                    queries.Add("offset", Convert.ToString(criteria.offset));
                }

                if (criteria.limit != null)
                {
                    queries.Add("limit", Convert.ToString(criteria.limit));
                }
                if(criteria.sortBy != null)
                {
                    queries.Add("sortBy", criteria.sortBy);
                }
            }

            var request = HttpRequestBuilder.CreateGet(BaseUrl + "/public/namespaces/{namespace}/items/byCriteria")
                .WithPathParam("namespace", Namespace_)
                .WithQueries(queries)
                .WithBearerAuth(AuthToken)
                .Accepts(MediaType.ApplicationJson)
                .GetResult();

            IHttpResponse response = null;

            yield return HttpClient.SendRequest(request, 
                rsp => response = rsp);

            var result = response.TryParseJson<ItemPagingSlicedResult>();

            callback.Try(result);
        }

    }
}
