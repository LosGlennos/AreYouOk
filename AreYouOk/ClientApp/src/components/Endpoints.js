import React, { Component } from 'react';
import { Button, Modal, ModalHeader, ModalBody, ModalFooter, Label, Input } from 'reactstrap';

export class Endpoints extends Component {
  static displayName = Endpoints.name;

  constructor(props) {
    super(props);

    this.state = {
      endpoints: [],
      modal: false,
    };

    this.toggle = this.toggle.bind(this);
  }

  componentWillMount() {
    this.fetchEndpoints().then(() => {
      console.log("fetched latest endpoints");
    });
  }

  async fetchEndpoints() {
    const response = await fetch('api/endpoints');
    const endpoints = await response.json();
    this.setState({ endpoints: endpoints });
  }

  toggle() {
    let modal = !this.state.modal;
    this.setState({ modal: modal });
  }

  render() {
    return (
      <div>
        <Button color="primary" onClick={this.toggle}>Add endpoint</Button>
        <ul style={{ paddingInlineStart: 0 }}>
          {this.state.endpoints.map((endpoint, index) =>
            <li className="list-group-item d-flex justify-content-between align-items-center" key={index}>
              Endpoint: {endpoint}
            </li>
          )}
        </ul>
        <Modal isOpen={this.state.modal} toggle={this.toggle}>
          <ModalHeader toggle={this.toggle}>Add new endpoint</ModalHeader>
          <ModalBody>
            <Label for="endpointUrl">Endpoint</Label>
            <Input type="password" name="password" id="endpointUrl" placeholder="URL" />
          </ModalBody>
          <ModalFooter>
            <Button color="primary" onClick={this.toggle}>Do Something</Button>{' '}
            <Button color="secondary" onClick={this.toggle}>Cancel</Button>
          </ModalFooter>
        </Modal>
      </div>)
  }
}
