export const CREATE_BUSINESS = "CREATE_BUSINESS";
export const UPDATE_BUSINESS = "UPDATE_BUSINESS";
export const DELETE_BUSINESS = "DELETE_BUSINESS";
export const SET_BUSINESSES = "SET_BUSINESSES";
export const GET_BUSINESSES = "GET_BUSINESSES";

export const createBusiness = (business, cb) => ({
  type: CREATE_BUSINESS,
  business,
  cb
});

export const updateBusiness = (business, cb) => ({
  type: UPDATE_BUSINESS,
  business,
  cb
});

export const deleteBusiness = (businessId) => ({
  type: DELETE_BUSINESS,
  businessId,
});

export const getBusinesses = () => ({
  type: GET_BUSINESSES,
});

export const setBusinesses = (businesses) => ({
  type: SET_BUSINESSES,
  businesses: businesses
});
