import { SaleDetail } from './sale-detail';

export interface Sale {
    saleId?: number,
    documentNumber?: string,
    paymentType: string,
    totalText: string,
    recordDate?: string,
    saleDetails: SaleDetail[]
}
