#if UseFirebaseAnalytic
using UnityEngine;
using System;
using Firebase.Analytics;
using System.Collections.Generic;
using System.Linq;

namespace Ensign.Unity.Firebase
{
    public class FbAnalytic : IAnalytic
    {
        public void Init()
        {
            Debug.LogWarning("FIREBASE Enabling data collection.");
            FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);

            // Set default session duration values.
            FirebaseAnalytics.SetSessionTimeoutDuration(new TimeSpan(0, 30, 0));
        }

        public void SetUserId(string userId)
        {
            FirebaseAnalytics.SetUserId(userId);
        }

        public void SetUserProperty(string name, string propertyValue)
        {
            FirebaseAnalytics.SetUserProperty(name, propertyValue);
        }

        public void TrackEvent(string eventName)
        {
            FirebaseAnalytics.LogEvent(eventName);
            // Log.Info($"TrackEvent({eventName})");
        }

        public void TrackEvent(string eventName, string value)
        {
            if(value == null)
                FirebaseAnalytics.LogEvent(eventName);
            else
                FirebaseAnalytics.LogEvent(eventName, "Value", value.ToString());
            // Log.Info($"TrackEvent({eventName})~{value}");
        }

        public void TrackEvent(string eventName, int value)
        {
            FirebaseAnalytics.LogEvent(eventName, "Value", value);
            // Log.Info($"TrackEvent({eventName})~{value}");
        }

        public void TrackEvent(string eventName, long value)
        {
            FirebaseAnalytics.LogEvent(eventName, "Value", value);
            // Log.Info($"TrackEvent({eventName})~{value}");
        }

        public void TrackEvent(string eventName, double value)
        {
            FirebaseAnalytics.LogEvent(eventName, "Value", value);
            // Log.Info($"TrackEvent({eventName})~{value}");
        }

        public void TrackEvent(string eventName, Dictionary<string, object> value)
        {
            if(value == null)
            {
                TrackEvent(eventName);
            }
            else
            {
                Parameter [] parameters = (from string key in value.Keys select new Parameter(key, value[key].ToString())).ToArray();
                FirebaseAnalytics.LogEvent(eventName, parameters);
                // string str = "";
                // foreach(var p in value.Keys)
                // {
                //     str += $"{p}={value[p]};";
                // }
                // Log.Info($"TrackEvent({eventName})~{str}");
            }
        }

        public void Transaction(string productId, string currencyCode, int quantity, double unitPrice, string transactionId, string receipt, string signature)
        {
            FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventPurchase, new Parameter[] {
                new Parameter("ProductId", productId),
                new Parameter("CurrencyCode", currencyCode),
                new Parameter("Quantity", quantity),
                new Parameter("UnitPrice", unitPrice),
                new Parameter("Receipt", receipt),
                new Parameter("Signature", signature)
            });
        }
    }
}
#endif