using System;
using System.Collections;
using System.Web;
using DPI.Interfaces;
using DPI.Services;

namespace Dpi.Central.Web.Account.Wireless.Processes.Rsp
{
    public sealed class ProductCompositionResolver
    {
        #region Classes

        private class Cach
        {
            public Cach()
            {
            }

            public object this[string key]
            {
                get { return HttpContext.Current.Session[key]; }
                set { HttpContext.Current.Session[key] = value; }
            }
        }

        #endregion

        #region Constants

        private const string MAIN_TO_FINALS_CK = "MAIN_TO_FINALS_CK";
        private const string FINAL_TO_PRODUCTS_CK = "FINAL_TO_PRODUCTS_CK";

        #endregion

        #region Fields

        private static Cach _cach = new Cach();

        #endregion

        #region Public Methods

        public static IWireless_Products[] GetCombinableProducts(IMap imap, IWireless_Products mainProduct, IWireless_Products[] optionalProducts)
        {
            ValidateParameters(imap, mainProduct, optionalProducts);
            int[] optionalProductIds = GetProductIds(optionalProducts);
            return GetCombinableProducts(imap, mainProduct.Wireless_product_id, optionalProductIds);
        }

        public static IWireless_Products[] GetMinimalCombinableProducts(IMap imap, IWireless_Products mainProduct, IWireless_Products[] optionalProducts) 
        {
            ValidateParameters(imap, mainProduct, optionalProducts);
            int[] optionalProductIds = GetProductIds(optionalProducts);
            return GetMinimalCombinableProducts(imap, mainProduct.Wireless_product_id, optionalProductIds);
        }

        #endregion

        #region Private Methods

        private static void ValidateParameters(IMap imap, IWireless_Products mainProduct, IWireless_Products[] optionalProducts)
        {
            if (imap == null) {
                throw new ArgumentNullException("imap");
            }

            if (mainProduct == null) {
                throw new ArgumentNullException("mainProduct");
            }

            if (optionalProducts == null) {
                throw new ArgumentNullException("optionalProducts");
            }
        }

        private static int[] GetProductIds(IWireless_Products[] products)
        {
            int[] productIds = new int[products.Length];
            for (int i = 0; i < products.Length; i++) {
                productIds[i] = products[i].Wireless_product_id;
            }

            return productIds;
        }

        private static bool ContainsAllProducts(IMap imap, IWireless_Products finalProduct, int[] productsToCheckIds) 
        {
            int combinableProductsCount;
            return ContainsAllProducts(imap, finalProduct, productsToCheckIds, out combinableProductsCount);
        }

        private static bool ContainsAllProducts(IMap imap, IWireless_Products finalProduct, int[] productsToCheckIds, out int combinableProductsCount)
        {
            IWireless_Products[] subProducts = GetSubProducts(imap, finalProduct.Wireless_product_id);

            Hashtable presentChecker = new Hashtable();
            foreach (IWireless_Products subProduct in subProducts) {
                presentChecker.Add(subProduct.Wireless_product_id, null);
            }

            bool containsAllProducts = true;
            foreach (int productToCheckId in productsToCheckIds) {
                containsAllProducts = presentChecker.Contains(productToCheckId);
                if (!containsAllProducts) {
                    break;
                }
            }

            if (containsAllProducts) {
                combinableProductsCount = subProducts.Length - productsToCheckIds.Length;
            } else {
                combinableProductsCount = int.MaxValue;
            }

            return containsAllProducts;
        }

        private static void UpdateCombinableProducts(IMap imap, ref Hashtable combinableProducts, int finalProductId, int mainProductId, int[] optionalProductIds)
        {
            ArrayList optionalProductIdsHolder = new ArrayList(optionalProductIds);

            IWireless_Products[] subProducts = GetSubProducts(imap, finalProductId);

            foreach (IWireless_Products subProduct in subProducts) {
                if (subProduct.Wireless_product_id == mainProductId) {
                    continue;
                }

                if (optionalProductIdsHolder.Contains(subProduct.Wireless_product_id)) {
                    continue;
                }

                if (combinableProducts.ContainsKey(subProduct.Wireless_product_id)) {
                    continue;
                }

                combinableProducts.Add(subProduct.Wireless_product_id, subProduct);
            }
        }

        private static IWireless_Products[] GetCombinableProducts(IMap imap, int mainProductId, int[] optionalProductIds)
        {
            IWireless_Products[] finalProducts = GetFinalProducts(imap, mainProductId);

            Hashtable combinableProducts = new Hashtable();

            foreach (IWireless_Products finalProduct in finalProducts) {
                bool containsAllOptionalProducts = ContainsAllProducts(imap, finalProduct, optionalProductIds);

                if (containsAllOptionalProducts) {
                    UpdateCombinableProducts(imap, ref combinableProducts, finalProduct.Wireless_product_id, mainProductId, optionalProductIds);
                }
            }

            IWireless_Products[] result = new IWireless_Products[combinableProducts.Count];
            combinableProducts.Values.CopyTo(result, 0);

            return result;
        }

        public static IWireless_Products[] GetMinimalCombinableProducts(IMap imap, int mainProductId, int[] optionalProductIds)
        {
            IWireless_Products[] finalProducts = GetFinalProducts(imap, mainProductId);

            IWireless_Products minimalFinalProduct = null;
            int minimalCombinableProductsCount = int.MaxValue;

            foreach (IWireless_Products finalProduct in finalProducts) {
                int combinableProductsCount;
                bool containsAllOptionalProducts = ContainsAllProducts(imap, finalProduct, optionalProductIds, out combinableProductsCount);

                if (containsAllOptionalProducts && combinableProductsCount < minimalCombinableProductsCount) {
                    minimalFinalProduct = finalProduct;
                    minimalCombinableProductsCount = combinableProductsCount;
                }
            }

            if (minimalFinalProduct == null) {
                return new IWireless_Products[0];
            }

            Hashtable combinableProducts = new Hashtable();
            UpdateCombinableProducts(imap, ref combinableProducts, minimalFinalProduct.Wireless_product_id, mainProductId, optionalProductIds);

            IWireless_Products[] result = new IWireless_Products[combinableProducts.Count];
            combinableProducts.Values.CopyTo(result, 0);

            return result;
        }

        private static Hashtable GetMainToFinalsMap()
        {
            Hashtable mainToFinalsMap = (Hashtable) _cach[MAIN_TO_FINALS_CK];

            if (mainToFinalsMap == null) {
                _cach[MAIN_TO_FINALS_CK] = mainToFinalsMap = new Hashtable();
            }

            return mainToFinalsMap;
        }

        private static Hashtable GetFinalToProductsMap()
        {
            Hashtable finalToProductsMap = (Hashtable) _cach[FINAL_TO_PRODUCTS_CK];

            if (finalToProductsMap == null) {
                _cach[FINAL_TO_PRODUCTS_CK] = finalToProductsMap = new Hashtable();
            }

            return finalToProductsMap;
        }

        private static IWireless_Products[] GetFinalProducts(IMap imap, int mainProductId)
        {
            Hashtable mainToFinalsMap = GetMainToFinalsMap();

            IWireless_Products[] finals = (IWireless_Products[]) mainToFinalsMap[mainProductId];

            if (finals == null) {
                finals = DpiWirelessSvc.GetFinalProducts(imap, mainProductId);
                mainToFinalsMap.Add(mainProductId, finals);
            }

            return finals;
        }

        private static IWireless_Products[] GetSubProducts(IMap imap, int final)
        {
            Hashtable finalToProductsMap = GetFinalToProductsMap();

            IWireless_Products[] subs = (IWireless_Products[]) finalToProductsMap[final];

            if (subs == null) {
                subs = DpiWirelessSvc.GetSubProducts(imap, final);
                finalToProductsMap.Add(final, subs);
            }

            return subs;
        }

        #endregion

        #region Constructors

        private ProductCompositionResolver()
        {
        }

        #endregion
    }
}