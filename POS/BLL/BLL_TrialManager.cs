using System;

namespace POS.BLL
{
    /// <summary>
    /// Manages trial period status and validation logic
    /// </summary>
    public static class BLL_TrialManager
    {
        /// <summary>
        /// Represents the trial status of the system
        /// </summary>
        public class TrialStatus
        {
            public bool IsLicensed { get; set; }
            public bool HasTrial { get; set; }
            public bool IsTrialActive { get; set; }
            public bool IsTrialExpired { get; set; }
            public DateTime? TrialStartDate { get; set; }
            public DateTime? TrialEndDate { get; set; }
            public int DaysRemaining { get; set; }
            public bool IsExpiringSoon => !IsLicensed && !IsTrialExpired && DaysRemaining <= 2 && DaysRemaining >= 0;
            public string StatusMessage { get; set; }
        }

        /// <summary>
        /// Gets the current trial status from the Business table
        /// IMPORTANT: Licensed systems IGNORE trial dates - they are mutually exclusive
        /// </summary>
        public static TrialStatus GetTrialStatus()
        {
            var status = new TrialStatus
            {
                IsLicensed = false,
                HasTrial = false,
                IsTrialActive = false,
                IsTrialExpired = false,
                TrialStartDate = null,
                TrialEndDate = null,
                DaysRemaining = 0,
                StatusMessage = "Unknown"
            };

            try
            {
                if (Main.DataSetApp?.Business == null || Main.DataSetApp.Business.Rows.Count == 0)
                {
                    status.StatusMessage = "No business data found";
                    return status;
                }

                var businessRow = Main.DataSetApp.Business[0];

                // Check licensed status FIRST - this takes precedence over everything
                if (!businessRow.Isis_licensedNull())
                {
                    string licensedValue = businessRow.is_licensed?.ToString();
                    status.IsLicensed = licensedValue == "True" || licensedValue == "1";
                }

                // If licensed, ignore all trial information
                if (status.IsLicensed)
                {
                    status.StatusMessage = "Licensed - Full Access";
                    status.HasTrial = false;
                    status.IsTrialActive = false;
                    status.IsTrialExpired = false;
                    status.DaysRemaining = 0;
                    return status;
                }

                // Only check trial dates if NOT licensed
                if (!businessRow.Istrial_start_dateNull() && !businessRow.Istrial_end_dateNull())
                {
                    string startDateStr = businessRow.trial_start_date?.ToString();
                    string endDateStr = businessRow.trial_end_date?.ToString();

                    if (!string.IsNullOrWhiteSpace(startDateStr) && !string.IsNullOrWhiteSpace(endDateStr))
                    {
                        if (DateTime.TryParse(startDateStr, out DateTime trialStart) &&
                            DateTime.TryParse(endDateStr, out DateTime trialEnd))
                        {
                            status.HasTrial = true;
                            status.TrialStartDate = trialStart;
                            status.TrialEndDate = trialEnd;

                            DateTime now = DateTime.Now.Date;
                            DateTime endDate = trialEnd.Date;

                            // Calculate days remaining
                            TimeSpan remaining = endDate - now;
                            status.DaysRemaining = (int)Math.Ceiling(remaining.TotalDays);

                            // Check if trial is active
                            status.IsTrialActive = now >= trialStart.Date && now <= endDate;
                            status.IsTrialExpired = now > endDate;

                            if (status.IsTrialExpired)
                            {
                                status.StatusMessage = "Trial Expired - License Required";
                            }
                            else if (status.IsExpiringSoon)
                            {
                                status.StatusMessage = $"Trial Expiring Soon - {status.DaysRemaining} day(s) remaining";
                            }
                            else if (status.IsTrialActive)
                            {
                                status.StatusMessage = $"Trial Active - {status.DaysRemaining} day(s) remaining";
                            }
                            else
                            {
                                status.StatusMessage = "Trial Not Started";
                            }
                        }
                    }
                }
                else
                {
                    // No trial dates and not licensed = requires license
                    status.StatusMessage = "No Trial Period - License Required";
                    status.IsTrialExpired = true; // Treat as expired to enforce restrictions
                }
            }
            catch (Exception ex)
            {
                status.StatusMessage = $"Error checking trial status: {ex.Message}";
            }

            return status;
        }

        /// <summary>
        /// Gets the number of days remaining in the trial period
        /// Returns 0 if licensed (trial doesn't matter)
        /// </summary>
        public static int GetDaysRemaining()
        {
            var status = GetTrialStatus();
            return status.IsLicensed ? 0 : status.DaysRemaining;
        }

        /// <summary>
        /// Checks if the trial period has expired
        /// Returns false if licensed (trial doesn't matter)
        /// </summary>
        public static bool IsTrialExpired()
        {
            var status = GetTrialStatus();
            // If licensed, trial status is irrelevant
            if (status.IsLicensed) return false;
            
            return status.IsTrialExpired;
        }

        /// <summary>
        /// Checks if the system is accessible (licensed OR active trial)
        /// Users can still login even if trial expired, but with restricted access
        /// </summary>
        public static bool IsSystemFullyAccessible()
        {
            var status = GetTrialStatus();
            // Licensed = always accessible
            // OR trial active = accessible
            return status.IsLicensed || status.IsTrialActive;
        }

        /// <summary>
        /// Checks if a warning should be shown to the user
        /// Returns false if licensed (no warnings needed)
        /// </summary>
        public static bool ShouldShowWarning()
        {
            var status = GetTrialStatus();
            // No warnings if licensed
            if (status.IsLicensed) return false;
            
            return status.IsExpiringSoon || status.IsTrialExpired;
        }

        /// <summary>
        /// Gets a user-friendly message about the trial status
        /// </summary>
        public static string GetStatusMessage()
        {
            return GetTrialStatus().StatusMessage;
        }

        /// <summary>
        /// Gets a formatted message for the trial expiration warning
        /// Returns empty string if licensed
        /// </summary>
        public static string GetWarningMessage()
        {
            var status = GetTrialStatus();

            // No warnings if licensed
            if (status.IsLicensed)
            {
                return string.Empty;
            }

            if (status.IsTrialExpired)
            {
                return "Your trial period has expired. Please activate a license to continue using all features.\n\n" +
                       "You can still access Business Settings to activate your license.";
            }

            if (status.IsExpiringSoon)
            {
                return $"Your trial period will expire in {status.DaysRemaining} day(s) on {status.TrialEndDate:MMM dd, yyyy}.\n\n" +
                       "Please activate a license soon to avoid service interruption.";
            }

            return string.Empty;
        }
    }
}
