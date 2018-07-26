"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var StringObject = /** @class */ (function () {
    function StringObject() {
    }
    return StringObject;
}());
exports.StringObject = StringObject;
var RequestStatus = /** @class */ (function () {
    function RequestStatus() {
        this.Removed = 'Removed';
        this.Inactive = 'Inactive';
        this.Sending = 'Sending';
        this.Accepted = 'Accepted';
        this.Rejected = 'Rejected';
        this.Waiting = "Waiting";
        this.Shipping = "Shipping";
        this.Unloading = "Unloading";
        this.Completed = "Completed";
    }
    return RequestStatus;
}());
exports.RequestStatus = RequestStatus;
var ShipmentStatus = /** @class */ (function () {
    function ShipmentStatus() {
        this.INACTIVE = "Inactive";
        this.WAITING = "Waiting";
        this.ACCEPTED = "Accepted";
        this.REJECTED = "Rejected";
        this.PICKING = "Picking";
        this.LOADING = "Loading";
        this.SHIPPING = "Shipping";
        this.COMPLETED = "Completed";
    }
    return ShipmentStatus;
}());
exports.ShipmentStatus = ShipmentStatus;
//# sourceMappingURL=utilities.js.map