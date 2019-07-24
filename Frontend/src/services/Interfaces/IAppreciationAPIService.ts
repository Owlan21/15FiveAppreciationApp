import Appreciation from '@/models/Appreciation';

export default interface IAppreciationAPIService {
    returnHighFives(): Promise<Appreciation>;
}