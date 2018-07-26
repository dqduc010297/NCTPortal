export class Request {
  customerName: string;
  pickingDate: string;
  deliveryDate: string;
  deliveryAddress: string;
  wereHouseAddress: string;
  packageQuantity: string;
  status: string
}

export interface Shipment {
  requestIdList: any[],
  requestQuantity: string;
  startDate: string,
  endDate: string,
  vehicleId: string,
  coordinatorId: string,
  driverId: string,
}
