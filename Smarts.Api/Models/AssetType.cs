﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Smarts.Api.Models
{
    /// <summary>
    /// A category for types of assets such as Articles, Videos, Courseware, etc.
    /// </summary>
    public class AssetType
    {
        #region Properties

        /// <summary>
        /// The unique id for the database record.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// The description of the asset type.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The name of the asset type.
        /// </summary>
        public string Name { get; set; }

        #endregion
    }
}