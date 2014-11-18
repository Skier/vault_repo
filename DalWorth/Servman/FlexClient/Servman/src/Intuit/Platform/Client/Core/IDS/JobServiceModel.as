
    /*******************************************************************
    * JobServiceModel.as
    * Copyright (C) 2006-2010 Midnight Coders, Inc.
    *
    * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
    * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
    * MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
    * NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
    * LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
    * OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
    * WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
    ********************************************************************/
    
    
    package Intuit.Platform.Client.Core.IDS
    {    
      import Intuit.Platform.Client.Core.vo.*;
      import Intuit.Platform.Client.Core.IDS.vo.*;
      import Intuit.Sb.Cdm.vo.*;
      import defaultPackage.vo.*;
      [Bindable]
      public class JobServiceModel
      {     
        public var AddJobResult:Job;     
        public var AddResourceResult:CdmBase;     
        public var FindAllResult:CdmComplexBase;     
        public var FindAllResult:Array;     
        public var FindByIdResult:Job;     
        public var FindByIdResult:CdmComplexBase;     
        public var GetJobsResult:Array;     
        public var GetResourcesResult:CdmComplexBase;     
        public var GetResourcesForQueryResult:CdmComplexBase;     
        public var RevertJobResult:Job;     
        public var RevertResourceResult:CdmBase;     
        public var UpdateJobResult:Job;     
        public var UpdateResourceResult:CdmBase;
      }
    }
  