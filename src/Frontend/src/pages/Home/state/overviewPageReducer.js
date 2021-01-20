export const LOADING_ACTION = "LOADING_ACTION";
export const SET_BUILDING = "SET_BUILDING";
export const GET_BUILDING_FAILED = "GET_BUILDING_FAILED";

export function overviewPageReducer(state, action) {
  if (action.type == LOADING_ACTION) {
    return { ...state, isLoading: true, isError: false, buildings: [] };
  }

  if (action.type == SET_BUILDING) {
    return { ...state, isLoading: false, buildings: action.payload };
  }

  if (action.type == GET_BUILDING_FAILED) {
    return { ...state, isLoading: false, isError: true };
  }

  return state;
}
