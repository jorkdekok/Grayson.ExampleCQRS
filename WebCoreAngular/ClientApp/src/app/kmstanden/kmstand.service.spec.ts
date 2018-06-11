import { TestBed, inject } from '@angular/core/testing';

import { KmstandService } from './kmstand.service';

describe('KmstandService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [KmstandService]
    });
  });

  it('should be created', inject([KmstandService], (service: KmstandService) => {
    expect(service).toBeTruthy();
  }));
});
