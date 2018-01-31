// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace Medikit.EHealth.ETK
{
    public enum ETKTypes
    {
        /// <summary>
        /// For RIZIV‐INAMI number, use type NIHII.
        /// </summary>
        NIHII = 0,
        /// <summary>
        /// For RIZIV‐INAMItype hospital, use type NIHII‐HOSPITAL.
        /// </summary>
        NIHIIHOSPITAL = 1,
        /// <summary>
        /// For company number use type CBE
        /// </summary>
        CBE = 2,
        /// <summary>
        /// For pharmacy numbersrecognized by the FAGG‐AFMPS use type NIHII‐PHARMACY
        /// </summary>
        NIHIIPHARMACY = 3,
        /// <summary>
        /// For Social Security numbers use type SSIN.
        /// </summary>
        SSIN = 4
    }
}
