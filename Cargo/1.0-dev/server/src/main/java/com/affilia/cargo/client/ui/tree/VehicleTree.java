/**
 * $Id: VehicleTree.java 279 2007-06-20 08:03:26Z moritur $
 */
package com.affilia.cargo.client.ui.tree;

import com.affilia.cargo.client.data.NetworkData;
import com.affilia.cargo.client.data.VehicleData;
import com.affilia.cargo.client.service.CargoServiceAsync;
import com.affilia.cargo.client.service.CargoServiceLocator;
import com.affilia.cargo.client.ui.VehicleInfoDialog;
import com.affilia.cargo.client.ui.map.CargoMapFactory;
import com.google.gwt.user.client.rpc.AsyncCallback;
import com.google.gwt.user.client.ui.Label;
import com.google.gwt.user.client.ui.Tree;
import com.google.gwt.user.client.ui.TreeItem;
import com.google.gwt.user.client.ui.TreeListener;
import com.google.gwt.user.client.ui.Widget;

public class VehicleTree 
    implements TreeListener {
    
    private static final String TREE_CSS = "ll-tree";
    private static final String SELECTED_ITEM_CSS = "ll-tree-selected-item";
    private static final String STANDART_ITEM_CSS = "ll-tree-standart-item";
    
    private static final int NO_ONE_ITEM_SELECT = -1;
    
    private Tree m_tree = new Tree();
    
    private CargoServiceAsync m_service = CargoServiceLocator.getService();
    
    private int m_selectNetworkItem = VehicleTree.NO_ONE_ITEM_SELECT;
    
    private CargoMapFactory m_cargoMapFactory;
    
    public VehicleTree(CargoMapFactory cargoMapFactory) {
        
        m_cargoMapFactory = cargoMapFactory;

        m_tree.addTreeListener(this);
        m_tree.setStyleName(VehicleTree.TREE_CSS);
        loadNetworks();
    }
    
    public Widget getWidget() {
        return m_tree;
    }

    public void onTreeItemSelected(TreeItem item) {
        
        if ( item instanceof VehicleTreeItem ) {
            
            new VehicleInfoDialog((VehicleData) item.getUserObject());
            
        } else if ( item instanceof NetworkTreeItem ) {
            
            int oldSelectNetworkItem = m_selectNetworkItem;
            m_selectNetworkItem = getNetworkItemIngex(item);
            
            if ( oldSelectNetworkItem != m_selectNetworkItem ) {
                if ( VehicleTree.NO_ONE_ITEM_SELECT != oldSelectNetworkItem ) {
                    TreeItem oldItem = m_tree.getItem(oldSelectNetworkItem);
                    oldItem.getWidget().setStyleName(VehicleTree.STANDART_ITEM_CSS);
                }
                item.getWidget().setStyleName(VehicleTree.SELECTED_ITEM_CSS);
                
                m_cargoMapFactory.createCargoMap(
                        ((NetworkTreeItem) item).networkId);
            }
        }
    
    }
    
    public void onTreeItemStateChanged(TreeItem item) {
        if ( item instanceof NetworkTreeItem ) {
            if ( item.getState() ) {
                loadAndAddVehicles((NetworkTreeItem) item);
            }
        }
    }
    
    public void reload() {
        loadNetworks();
    }
    
    private int getNetworkItemIngex(TreeItem item) {
        for (int i=0; i<m_tree.getItemCount(); i++ ) {
            if ( item.equals(m_tree.getItem(i)) ) {
                return i;
            }
        }
        return VehicleTree.NO_ONE_ITEM_SELECT;
    }
    
    private void loadNetworks() {
        AsyncCallback callback =  new AsyncCallback() {
            public void onFailure(Throwable caught) {
            }

            public void onSuccess(Object result) {
                NetworkData[] networks = (NetworkData[]) result;
                populateNetworks(networks);
            }
        };
        m_service.getAllNetworks(callback);
    }
    
    private void populateNetworks(NetworkData[] networks) {
        setNotMarckNetworks();
        for ( int i=0; i<networks.length; i++ ) {
            NetworkTreeItem networkItem = findNetworkTreeItem(networks[i]);
            if ( null != networkItem ) {
                storeNetworkTreeItem(networkItem, networks[i]);
            } else {
                createNetworkTreeItem(networks[i]);
            }
        }
        removeOldNetworkItems();
    }   
    
    private void removeOldNetworkItems() {
        for ( int i = m_tree.getItemCount() - 1; i>=0; i-- ) {
            NetworkTreeItem netItem = (NetworkTreeItem) m_tree.getItem(i);
            if ( false == netItem.isMarckeed ) {
                removeNetworkItem(i);
            }
        }
    }
    
    private void removeNetworkItem(int itenNum) {
        m_tree.removeItem(m_tree.getItem(itenNum));
        m_selectNetworkItem = VehicleTree.NO_ONE_ITEM_SELECT;
        m_cargoMapFactory.cleanCargoMap();
    }
    
    private void storeNetworkTreeItem(NetworkTreeItem networkItem, NetworkData network) {
        ((Label)  networkItem.getWidget()).setText(network.name);
        networkItem.isMarckeed = true;
        loadAndAddVehicles(networkItem);
    }
    
    private void createNetworkTreeItem(NetworkData network) {
      Label text = new Label(network.name);
      NetworkTreeItem networkItem = new NetworkTreeItem();
      networkItem.setWidget(text);
      m_tree.addItem(networkItem);
      networkItem.isMarckeed = true;
      networkItem.networkId = network.id;
      loadAndAddVehicles(networkItem);
    }
    
    
    private NetworkTreeItem findNetworkTreeItem(NetworkData network) {
        for ( int i = 0; i<m_tree.getItemCount(); i++ ) {
            NetworkTreeItem netItem = (NetworkTreeItem) m_tree.getItem(i);
            if ( network.id.intValue() == netItem.networkId.intValue() ) {
                return netItem;
            }
        }
        return null;
    }
    
    private void setNotMarckNetworks() {
        for ( int i = 0; i<m_tree.getItemCount(); i++ ) {
            NetworkTreeItem netItem = (NetworkTreeItem) m_tree.getItem(i);
            netItem.isMarckeed = false;
        }
    }
    
    
    private void loadAndAddVehicles(final NetworkTreeItem parent) {
        
        AsyncCallback callback =  new AsyncCallback() {
            public void onFailure(Throwable caught) {
            }

            public void onSuccess(Object result) {
                VehicleData[] vehicles = (VehicleData[]) result;
                populateVehicles(parent, vehicles);
            }
        };
        m_service.getVehiclesByNetwork(parent.networkId, callback);
    }
    
    private void populateVehicles(TreeItem item, VehicleData[] vehicles) {

        setNotMarckVehicles(item);
        for ( int i=0; i<vehicles.length; i++ ) {
            VehicleTreeItem vehicleItem = findVehicleTreeItem(item, vehicles[i]);
            if ( null != vehicleItem ) {
                storeVehicleTreeItem(vehicleItem, vehicles[i]);
            } else {
                createVehicleTreeItem(item, vehicles[i]);
            }
        }
        removeOldVehicleTreeItems(item);
        
    }
    
    private void removeOldVehicleTreeItems(TreeItem parent) {
        for ( int i = parent.getChildCount() - 1; i >= 0; i-- ) {
            VehicleTreeItem vehItem = (VehicleTreeItem) parent.getChild(i);
            if ( false == vehItem.isMarckeed ) {
                parent.removeItem(vehItem);
            }
        }
    }
    
    private void createVehicleTreeItem(TreeItem parent, VehicleData vehicle) {
        Label text = new Label(getVehicleItemText(vehicle));
        VehicleTreeItem vehicleItem = new VehicleTreeItem();
        vehicleItem.setWidget(text);
        vehicleItem.setUserObject(vehicle);
        parent.addItem(vehicleItem);
        vehicleItem.isMarckeed = true;
        vehicleItem.vehicleId = vehicle.id;
      }
    
    private void storeVehicleTreeItem(VehicleTreeItem vehicleItem, VehicleData vehicle) {
        ((Label)  vehicleItem.getWidget()).setText(getVehicleItemText(vehicle));
        vehicleItem.isMarckeed = true;
    }

    private String getVehicleItemText(VehicleData vehicle) {
        return vehicle.name + " [ " + vehicle.itemId + " ] ";
    }
    
    private VehicleTreeItem findVehicleTreeItem(TreeItem parent, VehicleData vehicle) {
        for ( int i = 0; i<parent.getChildCount(); i++ ) {
            VehicleTreeItem vehItem = (VehicleTreeItem) parent.getChild(i);
            if ( vehicle.id.intValue() == vehItem.vehicleId.intValue() ) {
                return vehItem;
            }
        }
        return null;
    }
    
    private void setNotMarckVehicles(TreeItem parent) {
        for ( int i = 0; i<parent.getChildCount(); i++ ) {
            VehicleTreeItem vehicleItem = (VehicleTreeItem) parent.getChild(i);
            vehicleItem.isMarckeed = false;
        }
    }

}
