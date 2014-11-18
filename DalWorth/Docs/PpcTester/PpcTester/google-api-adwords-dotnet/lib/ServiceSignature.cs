// Copyright 2010, Google Inc. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

// Author: api.anash@gmail.com (Anash P. Oommen)

using System;
using System.Collections.Generic;
using System.Text;

namespace com.google.api.adwords.lib {
  /// <summary>
  /// Defines an API signature object. This class is used as a support class
  /// to assist AdWordsUser in creating a service object.
  /// </summary>
  public class ServiceSignature {
    /// <summary>
    /// A unique id to distinguish the service represented by this signature
    /// object. This can be any value of your choice, and is used by
    /// AdWordsUser for tracking the services it created.
    /// </summary>
    public string id;

    /// <summary>
    /// The name of the service.
    /// </summary>
    public string serviceName;
  }
}
