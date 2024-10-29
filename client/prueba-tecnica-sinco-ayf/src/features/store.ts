import { configureStore } from "@reduxjs/toolkit";
import authReducer from "./authSlice";
import vehicleSummaryReducer from "./summarySlice";

const store = configureStore({
  reducer: {
    auth: authReducer,
    vehicleSummary: vehicleSummaryReducer,
  },
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;

export default store;
