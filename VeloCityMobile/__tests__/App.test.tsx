/**
 * @format
 */

import React from 'react';
import ReactTestRenderer from 'react-test-renderer';
import App from '../App';

const mockProps: any = {
  navigation: {
    navigate: jest.fn(),
    goBack: jest.fn(),
    setOptions: jest.fn(),
  },
  route: {
    params: {},
  },
};

test('renders correctly', async () => {
  await ReactTestRenderer.act(() => {
    ReactTestRenderer.create(<App {...mockProps} />);
  });
});
