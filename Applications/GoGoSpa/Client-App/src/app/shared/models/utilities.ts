export class StringObject {
  content: string;
}

export class RequestStatus {
  public readonly Removed = 'Removed';

  public readonly Inactive = 'Inactive';
  public readonly Sending = 'Sending';
  public readonly Accepted = 'Accepted';
  public readonly Rejected = 'Rejected';

  public readonly Waiting = "Waiting";
  public readonly Shipping = "Shipping";
  public readonly Unloading = "Unloading";
  public readonly Completed = "Completed";
}

export class ShipmentStatus {
  public readonly INACTIVE = "Inactive";
  public readonly WAITING = "Waiting";
  public readonly ACCEPTED = "Accepted";
  public readonly REJECTED = "Rejected";
  public readonly PICKING = "Picking";
  public readonly LOADING = "Loading";
  public readonly SHIPPING = "Shipping";
  public readonly COMPLETED = "Completed";
}
