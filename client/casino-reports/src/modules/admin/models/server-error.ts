export interface ServerError {
  headers: {
    normalizedNames: {},
    lazyUpdate: null,
  };
  status: number;
  statusText: string;
  url: string;
  ok: boolean;
  name: string;
  message: string;
  error: {
    errors: {
      [field: string]: string[],
    },
    title: string,
    status: number,
    traceId: string,
  };
}
